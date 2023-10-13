namespace jwt.Models
{
    public class PurchasesInvoiceDetailModel
    {

        public int ProductId { get; set; }



        public int ProductUnitId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        [JsonIgnore]
        public bool isEntering { get; set; } = true;
        [JsonIgnore]
        public decimal CostIn { get; set; } 
        public int? StoreId { get; set; }



    }
}
