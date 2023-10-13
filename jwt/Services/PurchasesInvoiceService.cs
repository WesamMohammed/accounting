using AutoMapper;

namespace jwt.Services
{
    public class PurchasesInvoiceService:IPurchasesInvoiceService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public PurchasesInvoiceService(ApplicationDbContext applicationDbContext,IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        public async Task<PurchasesInvoiceModel> AddPurchasesInvoiceAsync(PurchasesInvoiceModel purchasesInvoice)
        {
            

            foreach (var purchasesDetail in purchasesInvoice.InvoiceDetails)
            {
                if (purchasesDetail.StoreId is null)
                {
                    purchasesDetail.StoreId = purchasesInvoice.StoreId;
                }
                purchasesDetail.CostIn = purchasesDetail.UnitPrice;
            }

            var InvoiceMaster = _mapper.Map<InvoiceMaster>(purchasesInvoice);
            // var AccountingMaster = new AccountingMaster();
            InvoiceMaster.AccountingMaster = new AccountingMaster();
            InvoiceMaster.AccountingMaster.AccountingDetails = new List<AccountingDetail>();
            InvoiceMaster.AccountingMaster.Date = InvoiceMaster.Date;
            InvoiceMaster.AccountingMaster.OperationType = purchasesInvoice.OperationType;
            var creditorAccount = new AccountingDetail();
            creditorAccount.Amount = InvoiceMaster.Total;
            creditorAccount.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccount.AccountId = InvoiceMaster.AccountDainId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccount);

            var DebtorAccount = new AccountingDetail();
            DebtorAccount.Amount = InvoiceMaster.Total;
            DebtorAccount.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccount.AccountId = InvoiceMaster.AccountMadinId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccount);


            // InvoiceMaster.AccountingMaster=AccountingMaster;
            await _applicationDbContext.InvoiceMasters.AddAsync(InvoiceMaster);


            var result = await _applicationDbContext.SaveChangesAsync();
            return _mapper.Map<PurchasesInvoiceModel>(InvoiceMaster);



            

        }
              public async Task<SalesInvoiceModel>UpdateSalesInvoiceAsync(PurchasesInvoiceModel salesInvoice)
        {
     
            var invoiceMaster = await _applicationDbContext.InvoiceMasters.Include(a=>a.AccountingMaster).ThenInclude(a=>a.AccountingDetails).Include(a=>a.InvoiceDetails).FirstOrDefaultAsync(a=>a.Id==salesInvoice.Id);
            if (invoiceMaster == null)
            {
                return null;

            }
             _applicationDbContext.InvoiceDetails.RemoveRange(invoiceMaster.InvoiceDetails.ToList());
            _applicationDbContext.AccountingDetails.RemoveRange(invoiceMaster.AccountingMaster.AccountingDetails.ToList());
            decimal TotalCost = 0;
            foreach (var salesDetail in salesInvoice.InvoiceDetails)
            {
                if (salesDetail.StoreId is null)
                {
                    salesDetail.StoreId = salesInvoice.StoreId;
                }
           //     await this.Average_Cost(salesDetail,salesInvoice.Date);
             //   TotalCost = TotalCost + (salesDetail.CostOut * salesDetail.Quantity);
            }
            //  var InvoiceMaster = _mapper.Map<InvoiceMaster>(salesInvoice);
            // var AccountingMaster = new AccountingMaster();

            var InvoiceMaster = await _applicationDbContext.InvoiceMasters.Include(a => a.AccountingMaster).ThenInclude(a => a.AccountingDetails).Include(a => a.InvoiceDetails).FirstOrDefaultAsync(a => a.Id == salesInvoice.Id && a.OperationType==OperationType.PurchasesInvoice);
           // InvoiceMaster.AccountingMaster.Date = salesInvoice.Date;
            InvoiceMaster.InvoiceDetails = _mapper.Map<List<InvoiceDetail>>(salesInvoice.InvoiceDetails);
            InvoiceMaster.Description=salesInvoice.Description;
            InvoiceMaster.InvoiceType = salesInvoice.InvoiceType;
            InvoiceMaster.Date = salesInvoice.Date;
            InvoiceMaster.StoreId = salesInvoice.StoreId;
            InvoiceMaster.AccountDainId=salesInvoice.AccountDainId;
         //   InvoiceMaster.AccountMadinId = salesInvoice.AccountMadinId;
            InvoiceMaster.Total = salesInvoice.Total;
            InvoiceMaster.SupplierId = salesInvoice.SupplierId;

            //  InvoiceMaster.AccountingMaster.OperationType = InvoiceMaster.OperationType;
           InvoiceMaster.AccountingMaster.AccountingDetails = new List<AccountingDetail>();
           InvoiceMaster.AccountingMaster.OperationType = salesInvoice.OperationType;
            var creditorAccount = new AccountingDetail();
            creditorAccount.Amount = InvoiceMaster.Total;
            creditorAccount.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccount.AccountId = InvoiceMaster.AccountDainId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccount);

            var DebtorAccount = new AccountingDetail();
            DebtorAccount.Amount = InvoiceMaster.Total;
            DebtorAccount.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccount.AccountId = InvoiceMaster.AccountMadinId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccount);



            // InvoiceMaster.AccountingMaster=AccountingMaster;
         //   await _applicationDbContext.Update(InvoiceMaster);


            var result = await _applicationDbContext.SaveChangesAsync();



            /*if (SalesInvoice == null)
            {
                return null;
            }*/
            return _mapper.Map<SalesInvoiceModel>(InvoiceMaster);
        }
          public async Task<List<SalesInvoiceOutPutModel>> GetAllSalesInvoiceAsync(OperationType operationType,SalesInvoiceModel? salesInvoiceModel)
        {


            
            var SalesInvoice = await _applicationDbContext.InvoiceMasters.Include(a => a.InvoiceDetails).ThenInclude(a => a.ProductUnit).Include(a => a.InvoiceDetails).ThenInclude(a => a.Product).Include(a => a.InvoiceDetails).ThenInclude(a => a.Store).Include(a => a.AccountMadin).Include(a=>a.AccountDain).Include(a => a.Store).Include(a => a.Supplier).Where(a => a.OperationType == operationType
            && ((salesInvoiceModel.Id==null ?  true: a.Id == salesInvoiceModel.Id))).ToListAsync();

            var SalesInvoiceModel = _mapper.Map<List<SalesInvoiceOutPutModel>>(SalesInvoice);
            if (SalesInvoice is null)
                return null;
            return SalesInvoiceModel;

        }
                public async Task<SalesInvoiceModel> GetSalesInvoiceByIdAsync(int? id,OperationType operationType)
        {

            /*     var SalesInvoice = await _applicationDbContext.AccountingMasters.Include(a => a.InvoiceMaster).ThenInclude(a => a.InvoiceDetails).Select(a => a.InvoiceMaster).FirstOrDefaultAsync(a => a.OperationType == OperationType.SalesInvoice && a.Id == id); */
            var SalesInvoice = await _applicationDbContext.InvoiceMasters.Include(a => a.InvoiceDetails).FirstOrDefaultAsync(a => a.OperationType == operationType && a.Id == id);

            var SalesInvoiceModel = _mapper.Map<SalesInvoiceModel>(SalesInvoice);
            if (SalesInvoice is null)
                return null;
            return SalesInvoiceModel;

        }

    }
}
