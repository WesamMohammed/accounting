namespace jwt.Seeder
{
    public class AccountsSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AccountsSeeder> _logger;

        public AccountsSeeder(ApplicationDbContext dbContext, ILogger<AccountsSeeder> logger)
        {
            this._dbContext = dbContext;
            _logger = logger;


        }
        public async Task SeedDataAsync()
        {
            _logger.LogInformation($"########Start seeding#########");
            var accouns = await _dbContext.Set<Account>().ToListAsync();
            if(accouns.Any())
            {
                _logger.LogInformation($"######## {nameof(accouns)}: #########",accouns);
                return;
            }
            var newAccounts =new List<Account>(){
                new Account { Name="الأصول",Id=1,AccountNumber=1,AccountType=AccountType.Other,AppearIn=AppearIn.FINANCIAL},
                new Account { Name = "الخصوم", Id = 2, AccountNumber = 2, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME },
                new Account { Name = "الإيرادات", Id = 3, AccountNumber = 3, AccountType = AccountType.Other, AppearIn = AppearIn.FINANCIAL },
                new Account { Name = "المصروفات", Id = 4, AccountNumber = 4, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME },

            };
            _logger.LogInformation($"######## {nameof(newAccounts)}: #########", newAccounts);
            await _dbContext.Set<Account>().AddRangeAsync(newAccounts);
            await _dbContext.SaveChangesAsync();
            
        }
    }
}
