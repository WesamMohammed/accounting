namespace jwt.Entities
{
    public class AccountingDetail
    {
        public int Id { get; set; }
        public int AccountingId { get; set; }

        public AccountingMaster Accounting { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public CreditorOrDebtor CreditorOrDebtor { get; set; }

        public decimal Amount { get; set; }




    }
}
