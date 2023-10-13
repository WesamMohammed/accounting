using Microsoft.AspNetCore.Identity;
using jwt.Permissions;
using System.Security.Claims;

namespace jwt.Seeding
{
    public static class DefaultUser
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var DefaultAdminUser = new ApplicationUser()
            {
                FirstName="wesam",
                LastName="mohammed",
                Email="wesamalmoqry@gmail.com",
                UserName="wesammohammed",
                EmailConfirmed=true,
            };
            var PasswordDefaultAdminUser = "04340094Mohammed*";
            if (userManager.Users.All(u => u.Id != DefaultAdminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(DefaultAdminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(DefaultAdminUser, PasswordDefaultAdminUser);
                    await userManager.AddToRoleAsync(DefaultAdminUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(DefaultAdminUser, Roles.User.ToString());
                }

                 await roleManager.SeedClaimsForAdminRole();
            }

           
        }
        private static async Task SeedClaimsForAdminRole(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());

            await roleManager.AddPermissionClaim(adminRole, "Products");
            await roleManager.AddPermissionClaim(adminRole, "Sales");
            await roleManager.AddPermissionClaim(adminRole, "Purchases");
            await roleManager.AddPermissionClaim(adminRole, "Accounts");
            await roleManager.AddPermissionClaim(adminRole, "Customers");
            await roleManager.AddPermissionClaim(adminRole, "Roles");
            await roleManager.AddPermissionClaim(adminRole, "Users");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager,IdentityRole role, string module)
        {
            var allClaims= await roleManager.GetClaimsAsync(role);
            var Allpermissions = AppPermissions.GetPermmissionsForModule(module);
            foreach(var permission in Allpermissions)
            {
                if(!allClaims.Any(a=>a.Type=="Permissions"&& a.Value==permission))
                await roleManager.AddClaimAsync(role, new Claim("Permissions", permission));
            }
        }

    }
}
