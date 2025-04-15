using GamingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();  // Crée la base de données si elle n'existe pas

            // Si la table Products contient déjà des données, on ne fait rien
            if (context.Products.Any())
                return;

            // Ajouter des produits par défaut
            var products = new Product[]
            {
                new Product { Name = "PC Gamer RTX 4070", ImageUrl = "/images/pc1.jpg", Price = 1299.99M, Description = "Ultra performant avec RTX 4070." },
                new Product { Name = "PC Gamer i9 13900K", ImageUrl = "/images/pc2.jpg", Price = 1899.99M, Description = "Processeur Intel i9 pour gaming extrême." },
                new Product { Name = "PC Gamer Ryzen 9", ImageUrl = "/images/pc3.jpg", Price = 1499.99M, Description = "Performances AMD incroyables." }
            };

            context.Products.AddRange(products);  // Ajouter les produits à la base de données
            context.SaveChanges();  // Sauvegarder les modifications
        }
    }
}
