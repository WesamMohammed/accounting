namespace jwt.Permissions
{
    public static class AppPermissions
    {
        public static List<string> GetPermmissionsForModule(string module)
        {
            return new List<string>
            {
                $"Permission.{module}.Create",
                $"Permission.{module}.View",
                $"Permission.{module}.Edit",
                $"Permission.{module}.Delete",
            };


        }
       public static class products
        {
            public const string Create = $"Permission.Products.Create";
            public const string View = $"Permission.Products.View";
            public const string Edit = $"Permission.Products.Edit";
            public const string Delete = $"Permission.Products.Delete";
        }
        public static class Sales
        {
            public const string Create = $"Permission.Sales.Create";
            public const string View = $"Permission.Sales.View";
            public const string Edit = $"Permission.Sales.Edit";
            public const string Delete = $"Permission.Sales.Delete";
        }
        public static class Purchases
        {
            public const string Create = $"Permission.Purchases.Create";
            public const string View = $"Permission.Purchases.View";
            public const string Edit = $"Permission.Purchases.Edit";
            public const string Delete = $"Permission.Purchases.Delete";
        }
        public static class Accounts
        {
            public const string Create = $"Permission.Accounts.Create";
            public const string View = $"Permission.Accounts.View";
            public const string Edit = $"Permission.Accounts.Edit";
            public const string Delete = $"Permission.Accounts.Delete";
        }
             public static class Customers
        {
            public const string Create = $"Permission.Customers.Create";
            public const string View = $"Permission.Customers.View";
            public const string Edit = $"Permission.Customers.Edit";
            public const string Delete = $"Permission.Customers.Delete";
        }
        public static class Roles
        {
            public const string Create = $"Permission.Roles.Create";
            public const string View = $"Permission.Roles.View";
            public const string Edit = $"Permission.Roles.Edit";
            public const string Delete = $"Permission.Roles.Delete";
        }
        public static class Users
        {
            public const string Create = $"Permission.Users.Create";
            public const string View = $"Permission.Users.View";
            public const string Edit = $"Permission.Users.Edit";
            public const string Delete = $"Permission.Users.Delete";
        }
    }
}
