using System.Text.Json.Serialization;

namespace jwt.Models
{
    public class SalesInvoiceModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? IdForReturn { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public DateTime Date { get; set; }
      
        public int AccountDainId { get; set; } = 24;

     

        public int StoreId { get; set; }

        public int AccountMadinId { get; set; } = 41;
        [JsonIgnore]
        public OperationType OperationType { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted { get; set; }
        public int? CustomerId { get; set; }
        public int? SupplierId {get;set;}

     
        public ICollection<SalesInvoiceDetailModel> InvoiceDetails { get; set; }
        [JsonIgnore]
        public AccountingMasterModel AccountingMaster { get; set; }

        
    }
}
