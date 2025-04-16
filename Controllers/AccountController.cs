using GamingStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class AccountController : Controller
{
    // Simule une base de données pour l'exemple
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
        // Vérifier que le modèle est valide
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Recherche de l'utilisateur correspondant
        var user = users.FirstOrDefault(u =>
            (u.Email == model.EmailOrName || u.Email.Split('@')[0] == model.EmailOrName)
            && u.Password == model.Password);

        if (user.Email != null)
        {
            // Si l'utilisateur existe, on définit une session
            HttpContext.Session.SetString("IsAdmin", "true");

            // Redirige l'utilisateur vers la page des produits
            return RedirectToAction("Index", "Product");
        }

        // Si les informations sont incorrectes, on affiche un message d'erreur
        TempData["Error"] = "Nom/Gmail ou mot de passe incorrect.";
        return View(model);
    }

    public IActionResult Logout()
    {
        // Effacer la session lors de la déconnexion
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        return Content("Page de création de compte à implémenter.");
    }
}
