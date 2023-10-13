

using jwt.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace jwt.Services
{
    public class AccountService:IAccountService
    {   
          private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public AccountService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
         }

        public Task<AccountModel> AddAccount(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountModel> AddAccountAsync(AccountModel accountModel)
        {
                 var Paretn=await _applicationDbContext.Account.FirstOrDefaultAsync(x=> x.Id==accountModel.parentId);
            if (Paretn == null)
            {
                return null;
            }
            if (Paretn.IsSub)
            {
                return null;
            }
            int? maxAccountNumber = await _applicationDbContext.Account.Where(x => x.ParentId == Paretn.Id && x.IsSub == accountModel.isSub).MaxAsync(x=>(int?)x.AccountNumber);
         
            if (maxAccountNumber ==null)
            {
                maxAccountNumber = Paretn.AccountNumber;
                if (accountModel.isSub)
                {
                   maxAccountNumber=int.Parse(maxAccountNumber.ToString()+"0000");
                }
                else
                {
                    maxAccountNumber = int.Parse(maxAccountNumber.ToString() + "0");
                }
            }
            

            
            accountModel.accountNumber = (int)(++maxAccountNumber);
            accountModel.appearIn = Paretn.AppearIn;


            var account = _mapper.Map<Account>(accountModel);


            await _applicationDbContext.Account.AddAsync(account);
            if (account.AccountType == AccountType.Supplier)
            {
                var supplier = new Supplier()
                {
                    Name = account.Name,
                    Account=account,
                };
                await _applicationDbContext.Suppliers.AddAsync(supplier);
            }
            if (account.AccountType == AccountType.Customer)
            {
                var customer = new Customer()
                {
                    Name = account.Name,
                    Account = account,
                };
                await _applicationDbContext.Customers.AddAsync(customer);
            }
            var result =await _applicationDbContext.SaveChangesAsync();

            return _mapper.Map<AccountModel>(account);
        }
        public async Task<string> GetAllAccounts()
         {
            var Accounts =await  _applicationDbContext.Account.ToListAsync();
            var AccountOutPut = _mapper.Map<List<AccountOutPutModel>>(Accounts.Where(a => a.ParentId == null));
            var json = JsonConvert.SerializeObject(AccountOutPut,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            return json;
        }
       public async  Task<string> GetSubAccounts(InvoiceType? invoiceType){
                 var Accounts = await _applicationDbContext.Account.ToListAsync();
            var AccountOutPut = _mapper.Map<List<AccountOutPutModel>>(Accounts.Where(a => a.IsSub));
            if(invoiceType == InvoiceType.AGEL)
            {
                AccountOutPut= _mapper.Map<List<AccountOutPutModel>>(Accounts.Where(a => a.IsSub && (a.AccountType==AccountType.Customer|| a.AccountType==AccountType.Supplier)));
            }
            if (invoiceType == InvoiceType.NAKED)
            {
                AccountOutPut = _mapper.Map<List<AccountOutPutModel>>(Accounts.Where(a => a.IsSub && (a.AccountType == AccountType.Bank || a.AccountType == AccountType.Box )));
            }
            var json = JsonConvert.SerializeObject(AccountOutPut,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            return json;
         }

     
    }
}
      