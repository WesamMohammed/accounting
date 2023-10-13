namespace jwt.Entities
{
    public class AccountingMaster : BaseEntity
    {
        public int Id { get; set; }
        public OperationType OperationType { get; set; }
        public int ? InvoiceMasterId { get; set; }
        public InvoiceMaster InvoiceMaster { get; set; }


        public DateTime Date { get; set; }
        /*     public ICollection<AccountingDetail> AccountingDetails { get; set; }=new List<AccountingDetail>();*/
        public ICollection<AccountingDetail> AccountingDetails { get; set; }


    }
}
