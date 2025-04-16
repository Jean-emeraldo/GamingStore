using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du produit est requis.")]
        [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prix est requis.")]
        [Range(1, 10000, ErrorMessage = "Le prix doit être entre 1 et 10 000.")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Image du produit")]
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
