namespace jwt.Services
{
    public interface IPurchasesInvoiceService
    {
        Task<PurchasesInvoiceModel> AddPurchasesInvoiceAsync(PurchasesInvoiceModel purchasesInvoice);
        Task<List<SalesInvoiceOutPutModel>> GetAllSalesInvoiceAsync(OperationType operationType,SalesInvoiceModel? salesInvoiceModel);
        Task<SalesInvoiceModel> GetSalesInvoiceByIdAsync(int? id,OperationType operationType);
        Task<SalesInvoiceModel>UpdateSalesInvoiceAsync(PurchasesInvoiceModel salesInvoice);
    }
}
