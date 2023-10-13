using System.ComponentModel.DataAnnotations;
using jwt.Models;

namespace jwt.Entities
{
    public class Product:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public decimal? SellingPrice { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public long? Barcode { get; set; }
        public string? Description { get; set; }

        public ICollection<ProductUnit> ProductUnits { get; set; }


    }
}
