namespace jwt.Models
{
    public class SalesInvoiceDetailModel
    {

        
        public int ProductId { get; set; }
        

        
        public int ProductUnitId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool isEntering { get; set; }
        public int? StoreId { get; set; }
        [JsonIgnore]
        public decimal CostOut { get; set; } = 0;
        [JsonIgnore]
        public decimal CostIn { get; set; } = 0;


    }
}
