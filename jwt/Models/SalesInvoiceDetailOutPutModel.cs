namespace jwt.Models
{
    public class SalesInvoiceDetailOutPutModel
    {


        public ProductShort Product { get; set; }
        public int ProductId { get; set; }
        public ProductUnitShort ProductUnit { get; set; }

        public int ProductUnitId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Store Store { get; set; }

        /*public bool isEntering { get; set; } = false;*/

    }
}
