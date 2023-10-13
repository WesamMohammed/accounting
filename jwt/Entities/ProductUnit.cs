using System.ComponentModel.DataAnnotations;

namespace jwt.Entities
{
    public class ProductUnit
    {
        [Key]
        public int UnitId { get; set; }
     
        
        public string UnitName { get; set; }

        public decimal? UnitSellingPrice { get; set; }
        public decimal? UnitPurchasingPrice { get; set; }
        public long? UnitBarCode { get; set; }
        public decimal Ratio { get; set; }
        public int? Index { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
