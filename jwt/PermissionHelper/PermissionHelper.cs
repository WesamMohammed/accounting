using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace jwt.PermissionHelper
{
    public static class PermissionHelper
    {
        public static void GetPermissions(this List<RolePermissionsViewModel> allPermissions,Type policy,string? roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach(FieldInfo field in fields)
            {
                allPermissions.Add(new RolePermissionsViewModel { Type = "Permissions", Value = field.GetValue(null).ToString() });
            }
        }
        public static async Task AddPermission(this RoleManager<IdentityRole> roleManager,IdentityRole role,string permission)
        {
            var RolePermissions = await roleManager.GetClaimsAsync(role);
            if(!RolePermissions.Any(a=>a.Type=="Permissions"&& a.Value==permission))
                await roleManager.AddClaimAsync(role, new Claim("Permissions", permission));
        }
    }
}
