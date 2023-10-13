namespace jwt.Models
{
    public class SalesInvoiceOutPutModel
    {

        public int Id { get; set; }
        public string Description { get; set; }
        
        public DateTime Date { get; set; }

        public string InvoiceType { get; set; }
        public string OperationType { get; set; }


        public Store Store { get; set; }
        public AccountShort AccountMadin { get; set; }
        public int AccountMadinId { get; set; }
        public AccountShort AccountDain { get; set; }
        public int AccountDainId { get; set; }

        public decimal Total { get; set; }
        public bool IsDeleted { get; set; }
        public CustomerShort Customer { get; set; }
        public SupplierShort Supplier {get;set;}
        

        public ICollection<SalesInvoiceDetailOutPutModel> InvoiceDetails { get; set; }
    }
}
