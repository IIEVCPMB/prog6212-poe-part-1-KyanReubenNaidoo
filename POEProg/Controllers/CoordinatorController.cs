using Microsoft.AspNetCore.Mvc;
using POEProg.Data;
using POEProg.Models;

namespace POEProg.Controllers
{
    public class CoordinatorController : Controller
    {
        public IActionResult Index()
        {
            var pendingClaims = ClaimData.GetClaimsByStatus(ClaimStatus.Pending);
            return View(pendingClaims);
        }
        public IActionResult Verify(int id)
        {
            try
            {
                var claim = ClaimData.GetClaimById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                claim.Status = ClaimStatus.Verified;
                TempData["Message"] = $"Claim #{id} has been verified.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");
            }

        }
        public IActionResult Reject(int id)
        {
            try
            {
                var claim = ClaimData.GetClaimById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                claim.Status = ClaimStatus.Rejected;
                TempData["Message"] = $"Claim #{id} has been rejected.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
