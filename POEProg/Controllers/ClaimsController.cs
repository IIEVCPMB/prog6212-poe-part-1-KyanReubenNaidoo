using Microsoft.AspNetCore.Mvc;
using POEProg.Data;
using POEProg.Models;
using POEProg.Services;

namespace POEProg.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly FileEncryptionService _encryptionService;

        public ClaimsController(IWebHostEnvironment environment)
        {
            _environment = environment;
            _encryptionService = new FileEncryptionService();
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Lecturer")
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("Email");
            var claims = ClaimData.GetAllClaims()
                .Where(c => c.LecturerName == email).ToList();
            return View(claims);
        }

        public IActionResult SubmitClaim()
        {
            if (HttpContext.Session.GetString("Role") != "Lecturer")
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile uploadedFile)
        {
            var email = HttpContext.Session.GetString("Email");
            var lecturer = UserData.GetUserByEmail(email);
            if (lecturer == null) return RedirectToAction("Login", "Account");

            claim.LecturerName = lecturer.Email;
            claim.HourlyRate = (double)lecturer.HourlyRate;


            if (claim.HoursWorked > 180)
                ModelState.AddModelError("HoursWorked", "Cannot exceed 180 hours per month.");

            if (!ModelState.IsValid) return View(claim);

            claim.Total = Math.Round(claim.HoursWorked * claim.HourlyRate, 2);

            // File upload logic (same as before)
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var ext = Path.GetExtension(uploadedFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(ext))
                {
                    ViewBag.Error = "Invalid file type";
                    return View(claim);
                }
                if (uploadedFile.Length > 5_000_000)
                {
                    ViewBag.Error = "File too large (max 5MB)";
                    return View(claim);
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var encryptedName = Guid.NewGuid() + ".encrypted";
                var path = Path.Combine(uploadsFolder, encryptedName);

                using var stream = uploadedFile.OpenReadStream();
                await _encryptionService.EncryptFileAsync(stream, path);

                claim.Documents.Add(new Document
                {
                    FileName = uploadedFile.FileName,
                    EncryptedName = encryptedName,
                    Size = uploadedFile.Length,
                    UploadDate = DateTime.UtcNow
                });
            }

            ClaimData.AddClaim(claim);
            TempData["Success"] = "Claim submitted successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DownloadDocument(string encryptedName, string originalName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", encryptedName);
            if (!System.IO.File.Exists(filePath))
            {
                TempData["Error"] = "File not found";
                return RedirectToAction("Index");
            }

            var stream = await _encryptionService.DecryptFileAsync(filePath);
            return File(stream, "application/octet-stream", originalName);
        }
    }
}
