using System.ComponentModel.DataAnnotations;
namespace jwt.Models
{
    public class ProductUnitModel
    {
        public int UnitId { get; set; }
        [Required]
        public string UnitName { get; set; }

        public decimal? UnitSellingPrice { get; set; }
        public decimal? UnitPurchasingPrice { get; set; }
        public long? UnitBarCode { get; set; }
      
        [Required]
        public decimal Ratio { get; set; }
        [Required]
        public  int Index { get; set; }

    }
}
