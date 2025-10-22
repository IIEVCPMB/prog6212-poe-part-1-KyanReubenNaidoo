using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POEProg.Data;
using POEProg.Models;
using POEProg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                var claims = ClaimData.GetAllClaims();
                return View(claims);
            }
            catch (Exception)
            {
                ViewBag.Error = "Unable to load claims.";
                return View(new List<Claim>());
            }
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile uploadedFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(claim);
                }

                // Calculate total amount
                claim.Total = Math.Round(claim.HoursWorked * claim.HourlyRate, 2);

                // Handle file upload
                claim.Documents ??= new List<Document>();

                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                    var ext = Path.GetExtension(uploadedFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(ext))
                    {
                        ViewBag.Error = "Invalid file type. Only PDF, DOCX, XLSX allowed.";
                        return View(claim);
                    }

                    if (uploadedFile.Length > 5_000_000)
                    {
                        ViewBag.Error = "File too large (max 5MB).";
                        return View(claim);
                    }

                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var encryptedFileName = Guid.NewGuid() + ".encrypted";
                    var filePath = Path.Combine(uploadsFolder, encryptedFileName);

                    using (var stream = uploadedFile.OpenReadStream())
                    {
                        await _encryptionService.EncryptFileAsync(stream, filePath);
                    }

                    claim.Documents.Add(new Document
                    {
                        FileName = uploadedFile.FileName,
                        EncryptedName = encryptedFileName,
                        Size = uploadedFile.Length,
                        UploadDate = DateTime.UtcNow
                    });
                }

                // Add claim to in-memory data
                ClaimData.AddClaims(claim);

                TempData["Success"] = "Claim submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error submitting claim: " + ex.Message;
                return View(claim);
            }
        }
        public async Task<IActionResult> DownloadDocument(string encryptedName, string originalName)
        {
            try
            {
                string filePath = Path.Combine(_environment.WebRootPath, "uploads", encryptedName);
                if (!System.IO.File.Exists(filePath))
                {
                    TempData["Error"] = "File not found.";
                    return RedirectToAction(nameof(Index));
                }

                var stream = await _encryptionService.DecryptFileAsync(filePath);
                return File(stream, "application/octet-stream", originalName);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error downloading file: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
