using GamingStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class AccountController : Controller
{
    private readonly List<(string Email, string Password)> users = new()
    {
        ("admin@gmail.com", "admin123"),
        ("jean", "motdepasse")
    };

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = users.FirstOrDefault(u =>
            (u.Email == model.EmailOrName || u.Email.Split('@')[0] == model.EmailOrName)
            && u.Password == model.Password);

        if (user != default)
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Admin", "Product");
        }

        TempData["Error"] = "Nom/Gmail ou mot de passe incorrect.";
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        return Content("Page de création de compte à implémenter.");
    }

    public IActionResult Admin()
    {
        var isAdmin = HttpContext.Session.GetString("IsAdmin");
        if (isAdmin != "true")
        {
            return RedirectToAction("Login");
        }

        return View("Admin");
    }
}
