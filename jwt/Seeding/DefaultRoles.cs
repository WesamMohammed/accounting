using Microsoft.AspNetCore.Identity;

namespace jwt.Seeding
{
    public class DefaultRoles
    { 
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

        }
    }
}
