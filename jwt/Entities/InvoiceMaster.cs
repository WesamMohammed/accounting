namespace jwt.Entities
{
    public class InvoiceMaster:BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public DateTime Date { get; set; }
        public int AccountDainId { get; set; }

        public Account AccountDain { get; set; }
        public int AccountMadinId { get; set; }
        public Account AccountMadin { get; set; }
        public int? CustomerId { get; set; }
        public  Customer Customer { get; set; }
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
       // public int? AccountingMasterId { get; set; }
     //   public AccountingMaster AccountingMaster { get; set; } = new AccountingMaster();
        public AccountingMaster AccountingMaster { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        
        public OperationType OperationType { get; set; }
        public int? IdForReturn { get; set; }

        public decimal Total { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }


    }

    


}
