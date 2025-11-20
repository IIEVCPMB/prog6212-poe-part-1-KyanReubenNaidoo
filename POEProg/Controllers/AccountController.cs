using Microsoft.AspNetCore.Mvc;
using POEProg.Data;
using POEProg.Models;

namespace POEProg.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = UserData.GetUserByEmail(email);
            if (user == null || user.Password != password)
            {
                ViewBag.Error = "Invalid login.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Email", user.Email);

            return user.Role switch
            {
                "HR" => RedirectToAction("Index", "HR"),
                "Coordinator" => RedirectToAction("Index", "Coordinator"),
                "Manager" => RedirectToAction("Index", "Managers"),
                _ => RedirectToAction("Index", "Claims")
            };
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
