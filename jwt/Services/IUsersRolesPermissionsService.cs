using Microsoft.AspNetCore.Identity;

namespace jwt.Services
{
    public interface IUsersRolesPermissionsService
    {
        Task<List<User>> GetAllOtherUsers();
        Task<List<Role>> GetAllRoles();
        Task<IdentityRole> AddRole(ManageRolePermissionsViewModel model);
        Task<ManageUserRolesViewModel> GetUserRoles(string userId);
        Task<bool> UpdateUserRoles( ManageUserRolesViewModel model);
        Task<ManageRolePermissionsViewModel> GetRolePermissions(string RoleId);
        Task<bool> UpdateRolePermissions(ManageRolePermissionsViewModel model);
        Task<UserInfo> GetUserInf(string userId);
    }
}
