namespace jwt.Models
{
    public class CustomerModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
          public string? Email {get;set;}
        public string? Phone {get;set;}
        public string? Country {get;set;}
        public string? City {get;set;}
        
        
   
      public AccountCustomer Account { get; set; }
        
    }

    public class AccountCustomer{

      public int ParentId {get;set;}

    }
}
