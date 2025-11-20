using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using POEProg.Models;
using POEProg.Services;

namespace POEProg.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = UserData.GetUserByEmail(model.Email);
            if (user == null || user.Password != model.Password)
            {
                ViewBag.Error = "Invalid login.";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role.ToString());
            HttpContext.Session.SetString("Email", user.Email);

            return user.Role switch
            {
                Role.HR => RedirectToAction("Index", "HR"),
                Role.Coordinator => RedirectToAction("Index", "Coordinator"),
                Role.Manager => RedirectToAction("Index", "Managers"),
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
