namespace jwt.Models
{
    public class AccountingDetailModel
    {
        public int Id { get; set; }
        public int AccountingId { get; set; }

        
        public int AccountId { get; set; }
        
        public CreditorOrDebtor CreditorOrDebtor { get; set; }

        public decimal Amount { get; set; }
    }
}
