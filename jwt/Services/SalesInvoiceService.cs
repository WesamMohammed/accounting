using AutoMapper;
using jwt.Entities;
using jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace jwt.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public SalesInvoiceService(ApplicationDbContext applicationDbContext, IMapper mapper,IProductService productService)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _productService = productService;
        }
        public async Task<bool> AddSalesInvoiceAsync(SalesInvoiceModel salesInvoice)
        {
            decimal TotalCost = 0;
            foreach (var salesDetail in salesInvoice.InvoiceDetails)
            {
                if (salesDetail.StoreId is null)
                {
                    salesDetail.StoreId = salesInvoice.StoreId;
                }
                await this.Average_Cost(salesDetail,salesInvoice.Date);
                TotalCost = TotalCost +( salesDetail.CostOut*salesDetail.Quantity);
            }
         

            var InvoiceMaster = _mapper.Map<InvoiceMaster>(salesInvoice);
            InvoiceMaster.AccountingMaster = new AccountingMaster();

            InvoiceMaster.AccountingMaster.AccountingDetails = new List<AccountingDetail>();
            InvoiceMaster.AccountingMaster.Date = InvoiceMaster.Date;
            InvoiceMaster.AccountingMaster.OperationType = InvoiceMaster.OperationType;

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

            var creditorAccountCost = new AccountingDetail();
            creditorAccountCost.Amount = TotalCost;
            creditorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccountCost.AccountId = InvoiceMaster.AccountDainId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccountCost);

            var DebtorAccountCost = new AccountingDetail();
            DebtorAccountCost.Amount = TotalCost;
            DebtorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccountCost.AccountId = InvoiceMaster.AccountMadinId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccountCost);



            // InvoiceMaster.AccountingMaster=AccountingMaster;
            await _applicationDbContext.InvoiceMasters.AddAsync(InvoiceMaster);


            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;


        }

        public async Task<bool> AddReturneSalesAsync(SalesInvoiceModel salesInvoice)
        {

            decimal TotalCost = 0;
            foreach (var salesInvoiceDetail in salesInvoice.InvoiceDetails)
            {

                if (salesInvoiceDetail.StoreId is null)
                {
                    salesInvoiceDetail.StoreId = salesInvoice.StoreId;
                }
                var productInSale = await _applicationDbContext.InvoiceDetails.FirstOrDefaultAsync(a => a.ProductId == salesInvoiceDetail.ProductId && a.ProductUnitId == salesInvoiceDetail.ProductUnitId && a.InvoiceMaster.Id==salesInvoice.IdForReturn);
                if (productInSale is not null)
                {
                    salesInvoiceDetail.CostIn = (decimal)productInSale.CostOut;

                }
                else
                {
                    salesInvoiceDetail.CostIn = salesInvoiceDetail.UnitPrice;
                }
                TotalCost += salesInvoiceDetail.CostIn *salesInvoiceDetail.Quantity;
            }

            var InvoiceMaster = _mapper.Map<InvoiceMaster>(salesInvoice);
            // var AccountingMaster = new AccountingMaster();
            InvoiceMaster.AccountingMaster = new AccountingMaster();
            InvoiceMaster.AccountingMaster.AccountingDetails = new List<AccountingDetail>();
            InvoiceMaster.AccountingMaster.Date = InvoiceMaster.Date;
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
            var creditorAccountCost = new AccountingDetail();
            creditorAccountCost.Amount = TotalCost;
            creditorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccountCost.AccountId = 29;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccountCost);

            var DebtorAccountCost = new AccountingDetail();
            DebtorAccountCost.Amount = TotalCost;
            DebtorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccountCost.AccountId = 26;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccountCost);


            // InvoiceMaster.AccountingMaster=AccountingMaster;
            await _applicationDbContext.InvoiceMasters.AddAsync(InvoiceMaster);


            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;





        }

        public async Task<SalesInvoiceModel>UpdateSalesInvoiceAsync(SalesInvoiceModel salesInvoice)
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
                await this.Average_Cost(salesDetail,salesInvoice.Date);
                TotalCost = TotalCost + (salesDetail.CostOut * salesDetail.Quantity);
            }
            //  var InvoiceMaster = _mapper.Map<InvoiceMaster>(salesInvoice);
            // var AccountingMaster = new AccountingMaster();

            var InvoiceMaster = await _applicationDbContext.InvoiceMasters.Include(a => a.AccountingMaster).ThenInclude(a => a.AccountingDetails).Include(a => a.InvoiceDetails).FirstOrDefaultAsync(a => a.Id == salesInvoice.Id && a.OperationType==OperationType.SalesInvoice);
           // InvoiceMaster.AccountingMaster.Date = salesInvoice.Date;
            InvoiceMaster.InvoiceDetails = _mapper.Map<List<InvoiceDetail>>(salesInvoice.InvoiceDetails);
            InvoiceMaster.Description=salesInvoice.Description;
            InvoiceMaster.InvoiceType = salesInvoice.InvoiceType;
            InvoiceMaster.Date = salesInvoice.Date;
            InvoiceMaster.StoreId = salesInvoice.StoreId;
            InvoiceMaster.AccountMadinId = salesInvoice.AccountMadinId;
            InvoiceMaster.Total = salesInvoice.Total;
            InvoiceMaster.CustomerId = salesInvoice.CustomerId;

            //  InvoiceMaster.AccountingMaster.OperationType = InvoiceMaster.OperationType;
         //   InvoiceMaster.AccountingMaster.AccountingDetails = new List<AccountingDetail>();
            var creditorAccount = new AccountingDetail();
            creditorAccount.Amount = salesInvoice.Total;
            creditorAccount.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccount.AccountId = salesInvoice.AccountDainId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccount);

            var DebtorAccount = new AccountingDetail();
            DebtorAccount.Amount = salesInvoice.Total;
            DebtorAccount.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccount.AccountId = InvoiceMaster.AccountMadinId;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccount);
            var creditorAccountCost = new AccountingDetail();
            creditorAccountCost.Amount = TotalCost;
            creditorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccountCost.AccountId = 26;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccountCost);

            var DebtorAccountCost = new AccountingDetail();
            DebtorAccountCost.Amount = TotalCost;
            DebtorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccountCost.AccountId = 29;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccountCost);
            InvoiceMaster.AccountingMaster.Date = InvoiceMaster.Date;



            // InvoiceMaster.AccountingMaster=AccountingMaster;
         //   await _applicationDbContext.Update(InvoiceMaster);


            var result = await _applicationDbContext.SaveChangesAsync();



            /*if (SalesInvoice == null)
            {
                return null;
            }*/
            return _mapper.Map<SalesInvoiceModel>(InvoiceMaster);
        }
        public async Task<SalesInvoiceModel> UpdateReturnSalesAsync(SalesInvoiceModel salesInvoice)
        {

            var invoiceMaster = await _applicationDbContext.InvoiceMasters.Include(a => a.AccountingMaster).ThenInclude(a => a.AccountingDetails).Include(a => a.InvoiceDetails).FirstOrDefaultAsync(a => a.Id == salesInvoice.Id&& a.OperationType==OperationType.ReturnSalesInvoice);
            if (invoiceMaster == null)
            {
                return null;

            }
            _applicationDbContext.InvoiceDetails.RemoveRange(invoiceMaster.InvoiceDetails.ToList());    
            _applicationDbContext.AccountingDetails.RemoveRange(invoiceMaster.AccountingMaster.AccountingDetails.ToList());
            decimal TotalCost = 0;
            foreach (var salesInvoiceDetail in salesInvoice.InvoiceDetails)
            {

                if (salesInvoiceDetail.StoreId is null)
                {
                    salesInvoiceDetail.StoreId = salesInvoice.StoreId;
                }
                var productInSale = await _applicationDbContext.InvoiceDetails.FirstOrDefaultAsync(a => a.ProductId == salesInvoiceDetail.ProductId && a.ProductUnitId == salesInvoiceDetail.ProductUnitId && a.InvoiceMaster.Id == salesInvoice.IdForReturn);
                if (productInSale is not null)
                {
                    salesInvoiceDetail.CostIn = (decimal)productInSale.CostOut;

                }
                else
                {
                    salesInvoiceDetail.CostIn = salesInvoiceDetail.UnitPrice;
                }
                TotalCost += salesInvoiceDetail.CostIn * salesInvoiceDetail.Quantity;
            }
            var InvoiceMaster = await _applicationDbContext.InvoiceMasters.Include(a => a.AccountingMaster).ThenInclude(a => a.AccountingDetails).Include(a => a.InvoiceDetails).FirstOrDefaultAsync(a => a.Id == salesInvoice.Id && a.OperationType == OperationType.ReturnSalesInvoice);
            InvoiceMaster.InvoiceDetails = _mapper.Map<List<InvoiceDetail>>(salesInvoice.InvoiceDetails);
            InvoiceMaster.Description = salesInvoice.Description;
            InvoiceMaster.InvoiceType = salesInvoice.InvoiceType;
            InvoiceMaster.Date = salesInvoice.Date;
            InvoiceMaster.StoreId = salesInvoice.StoreId;
            InvoiceMaster.AccountMadinId = salesInvoice.AccountMadinId;
            InvoiceMaster.Total = salesInvoice.Total;
            InvoiceMaster.CustomerId = salesInvoice.CustomerId;
            InvoiceMaster.IdForReturn=salesInvoice.IdForReturn;

            InvoiceMaster.AccountingMaster.AccountingDetails = new List<AccountingDetail>();
            InvoiceMaster.AccountingMaster.Date = InvoiceMaster.Date;
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
            var creditorAccountCost = new AccountingDetail();
            creditorAccountCost.Amount = TotalCost;
            creditorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Creditor;
            creditorAccountCost.AccountId = 29;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(creditorAccountCost);

            var DebtorAccountCost = new AccountingDetail();
            DebtorAccountCost.Amount = TotalCost;
            DebtorAccountCost.CreditorOrDebtor = CreditorOrDebtor.Debtor;
            DebtorAccountCost.AccountId = 26;
            InvoiceMaster.AccountingMaster.AccountingDetails.Add(DebtorAccountCost);
            InvoiceMaster.AccountingMaster.Date = InvoiceMaster.Date;
            var result = await _applicationDbContext.SaveChangesAsync();
            return _mapper.Map<SalesInvoiceModel>(InvoiceMaster);


        }
        public async Task<List<SalesInvoiceOutPutModel>> GetAllSalesInvoiceAsync(OperationType operationType,SalesInvoiceModel? salesInvoiceModel)
        {


            
            var SalesInvoice = await _applicationDbContext.InvoiceMasters.Include(a => a.InvoiceDetails).ThenInclude(a => a.ProductUnit).Include(a => a.InvoiceDetails).ThenInclude(a => a.Product).Include(a => a.InvoiceDetails).ThenInclude(a => a.Store).Include(a => a.AccountMadin).Include(a=>a.AccountDain).Include(a => a.Store).Include(a => a.Customer).Where(a => a.OperationType == operationType
            && ((salesInvoiceModel.Id==null ?  true: a.Id == salesInvoiceModel.Id))).ToListAsync();

            var SalesInvoiceModel = _mapper.Map<List<SalesInvoiceOutPutModel>>(SalesInvoice);
            if (SalesInvoice is null)
                return null;
            return SalesInvoiceModel;

        }
        
        
        public async Task<List<SalesInvoiceOutPutModel>> GetAllSalesInvoiceAsync( OperationType operationType,SalesInvoiceModel? salesInvoiceModel ,int page=1,int pageSize=10,string sort="Id",bool isAscending=true)
        {


            
           List<InvoiceMaster> SalesInvoice = await _applicationDbContext.InvoiceMasters.Include(a => a.InvoiceDetails).ThenInclude(a => a.ProductUnit).Include(a => a.InvoiceDetails).ThenInclude(a => a.Product).Include(a => a.InvoiceDetails).ThenInclude(a => a.Store).Include(a => a.AccountMadin).Include(a=>a.AccountDain).Include(a => a.Store).Include(a => a.Customer).Where(a => a.OperationType == operationType
            && ((salesInvoiceModel.Id==null ?  true: a.Id == salesInvoiceModel.Id))).ToListAsync();
    var sortPropInfo=typeof(InvoiceMaster).GetProperty(sort);
    
            if(isAscending){
                 SalesInvoice=SalesInvoice.Skip((page-1)*pageSize).Take(pageSize).OrderBy(a=>sortPropInfo.GetValue(a,null)).ToList();
            }
            else{
                 SalesInvoice=SalesInvoice.Skip((page-1)*pageSize).Take(pageSize).OrderByDescending(a=>sortPropInfo.GetValue(a,null)).ToList();
            }

            var SalesInvoiceModel = _mapper.Map<List<SalesInvoiceOutPutModel>>(SalesInvoice);
            
            return SalesInvoiceModel;

        }
        
        /*public async Task<List<SalesInvoiceOutPutModel>> GetAllReturnSalesAsync()
        {



            var SalesInvoice = await _applicationDbContext.InvoiceMasters.Include(a => a.InvoiceDetails).ThenInclude(a => a.ProductUnit).Include(a => a.InvoiceDetails).ThenInclude(a => a.Product).Include(a => a.InvoiceDetails).ThenInclude(a => a.Store).Include(a => a.AccountMadin).Include(a=>a.AccountDain).Include(a => a.Store).Include(a => a.Customer).Where(a => a.OperationType == OperationType.SalesInvoice).ToListAsync();

            var SalesInvoiceModel = _mapper.Map<List<SalesInvoiceOutPutModel>>(SalesInvoice);
            if (SalesInvoice is null)
                return null;
            return SalesInvoiceModel;

        }*/
        public async Task<SalesInvoiceModel> GetSalesInvoiceByIdAsync(int? id,OperationType operationType)
        {

            /*     var SalesInvoice = await _applicationDbContext.AccountingMasters.Include(a => a.InvoiceMaster).ThenInclude(a => a.InvoiceDetails).Select(a => a.InvoiceMaster).FirstOrDefaultAsync(a => a.OperationType == OperationType.SalesInvoice && a.Id == id); */
            var SalesInvoice = await _applicationDbContext.InvoiceMasters.Include(a => a.InvoiceDetails).FirstOrDefaultAsync(a => a.OperationType == operationType && a.Id == id);

            var SalesInvoiceModel = _mapper.Map<SalesInvoiceModel>(SalesInvoice);
            if (SalesInvoice is null)
                return null;
            return SalesInvoiceModel;

        }
        public async Task Average_Cost(SalesInvoiceDetailModel invoiceDetail,DateTime Datebefore)
        {
            var product = await _productService.GetProductByIdAsync(invoiceDetail.ProductId);
            

            var income = await _applicationDbContext.InvoiceDetails.Where(a => a.ProductId == invoiceDetail.ProductId && a.isEntering == true&&a.InvoiceMaster.Date<Datebefore).GroupBy(a => true).Select(a =>
       new
       {

           quantityIn = (decimal)a.Sum(y => y.Quantity * y.ProductUnit.Ratio),
           costIn = (decimal)a.Sum(y => (y.CostIn / y.ProductUnit.Ratio) * (y.ProductUnit.Ratio * y.Quantity)),



       }).FirstOrDefaultAsync();
            var outcome = await _applicationDbContext.InvoiceDetails.Where(a => a.ProductId == invoiceDetail.ProductId && a.isEntering == false && a.InvoiceMaster.Date<Datebefore).GroupBy(a => true).Select(a =>
       new
       {

           quantityOut = (decimal)a.Sum(y => y.Quantity * y.ProductUnit.Ratio),
           costOut = (decimal)a.Sum(y => (y.CostOut / y.ProductUnit.Ratio) * (y.ProductUnit.Ratio * y.Quantity)),



       }).FirstOrDefaultAsync();

            decimal cost = 0;
            decimal quantity = 0;
            if (income is not null)
            {
                quantity = income.quantityIn;
                cost = income.costIn;
                if (outcome is not null)
                {
                    quantity = quantity - outcome.quantityOut;
                    cost = cost - outcome.costOut;

                }
            }

            if (quantity != 0)
            {
                cost = cost / quantity;
            }
            cost = cost * product.ProductUnits.Where(a => a.UnitId
            == invoiceDetail.ProductUnitId).FirstOrDefault().Ratio;

            /* var a = new
             {
                 cost = cost,
                 quantityAvailable = quantity,
                 income = income,
                 outcome = outcome,
                 p=product
             };
 */

            invoiceDetail.CostOut = cost;
            
        }


    }
}