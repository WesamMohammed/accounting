namespace jwt.Entities
{
    public class Supplier:BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email {get;set;}
        public string? Phone {get;set;}
        public string? Country {get;set;}
        public string? City {get;set;}
        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
