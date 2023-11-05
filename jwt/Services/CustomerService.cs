using AutoMapper;
using jwt.Models;
using Microsoft.AspNetCore.Mvc;


namespace jwt.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;

         }

        public async Task<List<CustomerModel>> GetAllCustomers(CustomerModel? customerModel)
        {
           
            var customers = await _applicationDbContext.Customers.Include(a => a.Account).Where(a => 
            ((customerModel.Id==null ?  true: a.Id == customerModel.Id)&&a.Account.IsSub)).ToListAsync();
            return _mapper.Map<List<CustomerModel>>(customers);
        }
        public async Task<List<CustomerModel>> GetAllSuppliersAsync()
        {
            var supplires = await _applicationDbContext.Suppliers.Include(a => a.Account).Where(a=>a.Account.IsSub).ToListAsync();
            return _mapper.Map<List<CustomerModel>>(supplires);
        }
        public async Task<CustomerModel> AddCustomer(CustomerModel customerModel){

             var Paretn=await _applicationDbContext.Account.FirstOrDefaultAsync(x=> x.Id==customerModel.Account.ParentId);

             int? maxAccountNumber = await _applicationDbContext.Account.Where(x => x.ParentId == Paretn.Id && x.IsSub == true).MaxAsync(x=>(int?)x.AccountNumber);
                if (maxAccountNumber ==null)
            {
                maxAccountNumber = Paretn.AccountNumber;
                
                
                   maxAccountNumber=int.Parse(maxAccountNumber.ToString()+"0000");
                
              
            }
            

            var customer=_mapper.Map<Customer>(customerModel);
            var account=new Account{
                Name=customerModel.Name,
                IsSub=true,
                AccountType=Paretn.AccountType,
                AppearIn=AppearIn.FINANCIAL,
                AccountNumber=(int)(++maxAccountNumber),
                ParentId=Paretn.Id,
                Customer=Paretn.AccountType==AccountType.Customer? customer:null,
                Supplier = Paretn.AccountType == AccountType.Supplier ? _mapper.Map<Supplier>(customerModel) : null,

            };
        await _applicationDbContext.Account.AddAsync(account);
        await _applicationDbContext.SaveChangesAsync();
        return _mapper.Map<CustomerModel>(customer);


        }
        public async Task<CustomerModel> UpdateCustomer(CustomerModel customerModel){
            var customer = await _applicationDbContext.Customers.Include(a=>a.Account).FirstOrDefaultAsync(a=>a.Id==customerModel.Id);
            var account= await _applicationDbContext.Account.Include(a=>a.Customer).FirstOrDefaultAsync(a=>a.Id==customer.AccountId);

            if(customerModel.Account.ParentId!=account.ParentId){
                return null;
            }

            account.Name=customerModel.Name;
            account.Customer.Name=customerModel.Name;
            account.Customer.Email=customerModel.Email;
            account.Customer.Phone=customerModel.Phone;
            account.Customer.Country=customerModel.Country;
            account.Customer.City=customerModel.City;

await _applicationDbContext.SaveChangesAsync();
return _mapper.Map<CustomerModel>(account.Customer);

        }
        
        public async Task<List<AccountShort>> GetMainAccountsCustomerType(){

            var accounts= await _applicationDbContext.Account.Where(a=>a.AccountType==AccountType.Customer&&a.IsSub==false).ToListAsync();
            return _mapper.Map<List<AccountShort>>(accounts);

        }
        public async Task<List<AccountShort>> GetMainAccountsSupplierTyse()
        {

            var accounts = await _applicationDbContext.Account.Where(a => a.AccountType == AccountType.Supplier && a.IsSub == false).ToListAsync();
            return _mapper.Map<List<AccountShort>>(accounts);

        }
    }
}
