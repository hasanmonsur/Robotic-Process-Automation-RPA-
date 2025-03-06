using Microsoft.AspNetCore.Mvc;
using RPAWebApp.Models;

namespace RPAWebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login page
        public IActionResult Login()
        {
            if (!ModelState.IsValid)
            {
                // ModelState has errors, return the view with the current ModelState
                return View();
            }

            return View();
        }

        // POST: Handle login logic
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            // Simulate a simple login check (you can replace this with actual logic)
            if (model.Username == "admin" && model.Password == "admin") // Example credentials
            {
                // Successful login
                return RedirectToAction("DownloadPage");
            }

            // Failed login, show error
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // GET: Download page
        public IActionResult DownloadPage()
        {
            return View();
        }

        // Action to download the PDF
        public IActionResult DownloadPdf()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "example.pdf");
            return PhysicalFile(filePath, "application/pdf", "example.pdf");
        }
    }
}
