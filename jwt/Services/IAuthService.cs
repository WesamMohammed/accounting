using jwt.Models;

namespace jwt.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);
        Task<AuthModel> LoginAsync(LoginModel loginModel);
        Task<string> AddRoleToUser(AddRoleToUserModel addRoleToUserModel);

    }
}
