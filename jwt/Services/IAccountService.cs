using jwt.Models;

namespace jwt.Services
{
    public interface IAccountService
    {
        Task<AccountModel> AddAccountAsync(AccountModel accountModel);
        Task<string> GetAllAccounts();
        Task<string> GetSubAccounts(InvoiceType? invoiceType);

    }
}
