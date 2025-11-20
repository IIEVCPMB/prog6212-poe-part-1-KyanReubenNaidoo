using Microsoft.AspNetCore.Mvc;
using POEProg.Data;
using POEProg.Models;

namespace POEProg.Controllers
{
    public class ManagersController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Manager")
                return RedirectToAction("Login", "Account");

            var verifiedClaims = ClaimData.GetClaimsByStatus(ClaimStatus.Verified);
            return View(verifiedClaims);
        }

        public IActionResult Approve(int id)
        {
            var claim = ClaimData.GetClaimById(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Approved;
            TempData["Message"] = $"Claim #{id} approved.";
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
