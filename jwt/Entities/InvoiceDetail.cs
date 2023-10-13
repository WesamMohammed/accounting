namespace jwt.Entities
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceMasterId { get; set; }
        public InvoiceMaster InvoiceMaster { get; set; }
         public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductUnitId { get; set; }

        public ProductUnit ProductUnit { get; set; }
        
        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public bool isEntering { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public decimal? CostIn { get; set; } = 0;
        public decimal? CostOut { get; set; } = 0;







    }
}
