using jwt.Seeder;

namespace jwt.EfCoreExtensions
{
    public static class EfCoreExtension
    {
        public static  IApplicationBuilder UseCustomMigration(this IApplicationBuilder app)
        {
           using var scope=app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                 dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                

            }
            return app;
        }

        public static IApplicationBuilder UseCustomSeeder(this IApplicationBuilder app)
        {
             using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var accounseeder = services.GetRequiredService<IDataSeeder>();
             accounseeder.SeedDataAsync().GetAwaiter().GetResult();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("app");
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                 DefaultRoles.SeedRolesAsync(userManager, roleManager).GetAwaiter().GetResult();
                 DefaultUser.SeedAdminUserAsync(userManager, roleManager).GetAwaiter().GetResult();
                logger.LogInformation("Finished Seeding Default Data");
                logger.LogInformation("Application Starting");

            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred seeding the DB");
            }
            return app;
        }
    }
}
