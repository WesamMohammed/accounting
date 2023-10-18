namespace jwt.Seeder
{
    public class AccountsSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        

        public AccountsSeeder(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            


        }
        public async Task SeedDataAsync()
        {
            var accouns = await _dbContext.Set<Account>().ToListAsync();
            if(accouns.Any())
            {
                return;
            }
            var newAccounts =new List<Account>(){
                new Account { Name="الأصول",Id=1,AccountNumber=1,AccountType=AccountType.Other,AppearIn=AppearIn.FINANCIAL},
                new Account { Name = "الخصوم", Id = 2, AccountNumber = 2, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME },
                new Account { Name = "الإيرادات", Id = 3, AccountNumber = 3, AccountType = AccountType.Other, AppearIn = AppearIn.FINANCIAL },
                new Account { Name = "المصروفات", Id = 4, AccountNumber = 4, AccountType = AccountType.Other, AppearIn = AppearIn.INCOME },

            };
            await _dbContext.Set<Account>().AddRangeAsync(newAccounts);
            
        }
    }
}
