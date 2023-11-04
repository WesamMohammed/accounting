using Microsoft.Extensions.Logging;
using System.Xml.Linq;

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
                new Account { Name="الأصول",AccountNumber=01,AccountType=AccountType.Other,AppearIn=AppearIn.FINANCIAL,ParentId=null,IsSub=false,Children=new List<Account>()
                {
                    new Account{Name= "المخزون",AccountNumber=000001}
                }
                },
                new Account { Name = "الخصوم", AccountNumber = 02, AccountType = AccountType.Other, AppearIn = AppearIn.FINANCIAL ,ParentId=null,IsSub=false},
                new Account { Name = "الإيرادات", AccountNumber = 03, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME ,ParentId=null,IsSub=false,
                    Children = new List < Account >(){
                    new Account(){ Name="المبيعات",AccountNumber=000003,AccountType=AccountType.Other,AppearIn=AppearIn.FINANCIAL,ParentId=null,IsSub=true,
                    
                },
                } },
                new Account { Name = "المصروفات", AccountNumber = 04, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME ,ParentId=null,IsSub=false,
                 Children = new List < Account >(){
                    new Account(){ Name="المشتريات",AccountNumber=000004,AccountType=AccountType.Other,AppearIn=AppearIn.FINANCIAL,ParentId=null,IsSub=true,
                    }
                },

                }

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
