using jwt.Entities;
using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public bool IsDeleted { get; set; }

        public decimal? SellingPrice { get; set; }

        public decimal? PurchasingPrice { get; set; }
        public long? Barcode { get; set; }
        public string? Description { get; set; } 
        [Required]
        public ICollection<ProductUnitModel> ProductUnits { get; set; }

    }
}
