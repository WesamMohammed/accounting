using System.Text.Json.Serialization;

namespace jwt.Models
{
    public class PurchasesInvoiceModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public int AccountMadinId { get; set; } 

        public int StoreId { get; set; }

        public int AccountDainId { get; set; }
        [JsonIgnore]
        public OperationType OperationType { get; set; } = OperationType.PurchasesInvoice;

        public decimal Total { get; set; }
        public bool IsDeleted { get; set; }
        public int? SupplierId { get; set; }


        public ICollection<PurchasesInvoiceDetailModel> InvoiceDetails { get; set; }


    }
}
