using Microsoft.AspNetCore.Mvc;
using POEProg.Data;
using POEProg.Models;

namespace POEProg.Controllers
{
    public class CoordinatorController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Coordinator")
                return RedirectToAction("Login", "Account");

            var pendingClaims = ClaimData.GetClaimsByStatus(ClaimStatus.Pending);
            return View(pendingClaims);
        }

        public IActionResult Verify(int id)
        {
            var claim = ClaimData.GetClaimById(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Verified;
            TempData["Message"] = $"Claim #{id} verified.";
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var claim = ClaimData.GetClaimById(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            TempData["Message"] = $"Claim #{id} rejected.";
            return RedirectToAction("Index");
        }
    }
}
