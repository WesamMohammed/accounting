﻿using Microsoft.AspNetCore.Mvc;

namespace jwt.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetAllCustomers(CustomerModel? customerModel);
        Task<List<CustomerModel>> GetAllSuppliersAsync();
         Task<CustomerModel> AddCustomer(CustomerModel customerModel);
          Task<List<AccountShort>> GetMainAccountsCustomerType();
         Task<List<AccountShort>> GetMainAccountsSupplierTyse();
          Task<CustomerModel> UpdateCustomer(CustomerModel customerModel);
        Task<CustomerModel> GetById(int id, AccountType type);
    }
}
