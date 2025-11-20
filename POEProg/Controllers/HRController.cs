using Microsoft.AspNetCore.Mvc;
using POEProg.Data;
using POEProg.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace POEProg.Controllers
{
    public class HRController : Controller
    {
        public IActionResult Index()
        {
            var check = HttpContext.Session.GetString("Role");
            if (check != "HR") return RedirectToAction("Login", "Account");

            var users = UserData.GetAllUsers();
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid) return View(user);
            UserData.AddUser(user);
            TempData["Success"] = "User created successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = UserData.GetAllUsers().FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid) return View(user);
            UserData.UpdateUser(user);
            TempData["Success"] = "User updated successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult GeneratePDF()
        {
            var users = UserData.GetAllUsers();
            var doc = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Users Report").Bold().FontSize(20).AlignCenter();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ID");
                            header.Cell().Text("Name");
                            header.Cell().Text("Surname");
                            header.Cell().Text("Email");
                            header.Cell().Text("Rate");
                        });

                        foreach (var user in users)
                        {
                            table.Cell().Text(user.Id.ToString());
                            table.Cell().Text(user.Name);
                            table.Cell().Text(user.Surname);
                            table.Cell().Text(user.Email);
                            table.Cell().Text(user.HourlyRate.ToString("C"));
                        }
                    });
                });
            });

            var stream = new MemoryStream();
            doc.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream, "application/pdf", "UsersReport.pdf");
        }
    }
}
