using Microsoft.AspNetCore.Mvc;
using GamingStore.Data;
using GamingStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult NewPage() => View();

        public IActionResult Apropos() => View();

        public IActionResult Create() => View();

        public async Task<IActionResult> Admin()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Le formulaire est invalide.";
                return View(product);
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                TempData["Error"] = "Veuillez sélectionner une image.";
                return View(product);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                TempData["Error"] = "Le fichier n'est pas une image valide.";
                return View(product);
            }

            var fileName = Path.GetFileName(imageFile.FileName);
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            product.ImageUrl = "/images/" + fileName;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Produit ajouté avec succès.";
            return RedirectToAction("Index");
        }

        public IActionResult Buy(int id)
        {
            var product = GetProductById(id);
            if (product == null)
            {
                TempData["Error"] = "Produit introuvable.";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult ConfirmBuy(int productId, string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Veuillez remplir tous les champs.");
            }

            var product = GetProductById(productId);
            if (product == null)
            {
                TempData["Error"] = "Produit introuvable.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View("Buy", product);
            }

            if (account == "test" && password == "1234")
            {
                TempData["Success"] = "Achat confirmé !";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Compte ou mot de passe incorrect.";
            return View("Buy", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Produit introuvable.";
                return RedirectToAction("Index");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Produit supprimé avec succès.";
            return RedirectToAction("Index");
        }

        private Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
