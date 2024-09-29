using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class CreateProductModel
    {
        //[MaxLength(200)]
        //[Range(0, 100)]
        //[EmailAddress]

        [Required, MaxLength(200), MinLength(3)]
        public string Name { get; set; } = null!;
        [Url]
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
    }
}
