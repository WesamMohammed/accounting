using jwt.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwt.Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options)
        {
                
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            /*modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique(true);

            modelBuilder.Entity<ProductUnit>().HasIndex(x => new { x.ProductId, x.UnitName }).IsUnique(true);*/
            modelBuilder.Entity<ProductUnit>().HasIndex(x => x.UnitBarCode).IsUnique(true);
         /*   modelBuilder.Entity<InvoiceMaster>().HasOne(a => a.AccountingMaster).WithOne(a => a.InvoiceMaster).IsRequired(false).HasForeignKey<InvoiceMaster>(a => a.AccountingMasterId).IsRequired(false);*/


        }



        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Categorys { set; get; }
        public DbSet<ProductUnit> productUnits { get; set; }
        public DbSet<Account> Account { set; get; }
        public DbSet<InvoiceMaster> InvoiceMasters { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { set; get; }
       
        public DbSet<AccountingMaster> AccountingMasters { set; get; }
        public DbSet<AccountingDetail> AccountingDetails { set; get; }
        public DbSet<Store> Stores { set; get; }







    }
    public class db : DbContext
    {
        public db(IOptions<db> options)
        {

        }
        public db()
        {
            
        }
        public DbSet<Product> MyProperty { get; set; }
    }
}
