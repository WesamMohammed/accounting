using Microsoft.Extensions.Logging;

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
                _logger.LogInformation($"######## {nameof(accouns)}:{accouns} #########");
                return;
            }
            var newAccounts =new List<Account>(){
                new Account { Name="الأصول",AccountNumber=1,AccountType=AccountType.Other,AppearIn=AppearIn.FINANCIAL,ParentId=null,IsSub=false},
                new Account { Name = "الخصوم", AccountNumber = 2, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME ,ParentId=null,IsSub=false},
                new Account { Name = "الإيرادات", AccountNumber = 3, AccountType = AccountType.Other, AppearIn = AppearIn.FINANCIAL ,ParentId=null,IsSub=false},
                new Account { Name = "المصروفات", AccountNumber = 4, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME ,ParentId=null,IsSub=false}

            };
            _logger.LogInformation($"######## {nameof(newAccounts)}: {newAccounts} #########", newAccounts);
            await _dbContext.Set<Account>().AddRangeAsync(newAccounts);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Message:{ex.Message}   erros:{ex.InnerException}");
            }
            
        }
    }
}
