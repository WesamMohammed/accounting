namespace jwt.Models
{
    public class AccountingMasterModel
    {
        public int Id { get; set; }
        public OperationType OperationType { get; set; }
        public int? InvoiceMasterId { get; set; }
        


        public DateTime Date { get; set; }
        public ICollection<AccountingDetailModel> AccountingDetails { get; set; }
    }
}
