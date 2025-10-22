using Microsoft.AspNetCore.Mvc;
using POEProg.Models;
using POEProg.Data;

namespace POEProg.Controllers
{
    public class ManagersController : Controller
    {
        public IActionResult Index()
        {
            var verifiedClaims = ClaimData.GetClaimsByStatus(ClaimStatus.Verified);
            return View(verifiedClaims);
        }

        public IActionResult Approve(int id)
        {
            try
            {
                var claim = ClaimData.GetClaimById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                claim.Status = ClaimStatus.Approved;
                TempData["Message"] = $"Claim #{id} has been approved.";
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
