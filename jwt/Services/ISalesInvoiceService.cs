using jwt.Entities;
using jwt.Models;

namespace jwt.Services
{
    public interface ISalesInvoiceService
    {
        Task<bool> AddSalesInvoiceAsync(SalesInvoiceModel salesInvoice);
       Task<List<SalesInvoiceOutPutModel>> GetAllSalesInvoiceAsync(OperationType operationType,SalesInvoiceModel salesInvoiceModel);
        Task<SalesInvoiceModel> GetSalesInvoiceByIdAsync(int? id,OperationType operationType);
        Task<SalesInvoiceModel> UpdateSalesInvoiceAsync(SalesInvoiceModel salesInvoice);
        Task<bool> AddReturneSalesAsync(SalesInvoiceModel salesInvoice);
        Task<SalesInvoiceModel> UpdateReturnSalesAsync(SalesInvoiceModel salesInvoice);
        Task<List<SalesInvoiceOutPutModel>> GetAllSalesInvoiceAsync( OperationType operationType,SalesInvoiceModel? salesInvoiceModel ,int page=1,int pageSize=10,string sort="Id",bool isAscending=true);
    }
}
