namespace jwt.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AccountNumber {get;set; }

        public bool IsSub { get; set; }

        public AppearIn AppearIn { get; set; }
        public AccountType AccountType { get; set; }

        public int? ParentId { get; set; }
        public Account Parent { get; set; }

        public ICollection<Account> Children { get; set; }
        public Customer Customer {get;set;}
        public Supplier Supplier {get;set;}
        

        


    }
  
}
