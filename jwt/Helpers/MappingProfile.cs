using jwt.Models;
using AutoMapper;
using jwt.Entities;

namespace jwt.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Product, ProductShort>().ReverseMap();
            CreateMap<ProductUnit, ProductUnitModel>().ReverseMap();
            CreateMap<ProductUnit, ProductUnitShort>().ReverseMap();
            CreateMap<Account, AccountModel>().ReverseMap();
            CreateMap<Account, AccountShort>().ReverseMap();
            CreateMap<InvoiceMaster,SalesInvoiceModel>().ReverseMap();
            CreateMap<InvoiceDetail, SalesInvoiceDetailModel>().ReverseMap();
            CreateMap<InvoiceMaster, SalesInvoiceOutPutModel>().ForMember(a=>a.OperationType,b=>b.MapFrom(a=>GetOperationType(a.OperationType))).ForMember(a=>a.InvoiceType,b=>b.MapFrom(a=>(GetInvoiceType(a.InvoiceType)))).ReverseMap();
            CreateMap<InvoiceDetail, SalesInvoiceDetailOutPutModel>().ReverseMap();
            CreateMap<Account, AccountOutPutModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<InvoiceMaster,PurchasesInvoiceModel>().ForMember(a => a.OperationType, b => b.MapFrom(a => GetOperationType(a.OperationType))).ForMember(a => a.InvoiceType, b => b.MapFrom(a => (GetInvoiceType(a.InvoiceType)))).ReverseMap();
            CreateMap<InvoiceDetail, PurchasesInvoiceDetailModel>().ReverseMap();
            CreateMap<Supplier, CustomerModel>().ReverseMap();
            CreateMap<IdentityRole, Role>().ForMember(a => a.RoleId, b => b.MapFrom(a => a.Id)).ForMember(a => a.RoleName, b => b.MapFrom(a => a.Name)).ReverseMap();
            CreateMap<ApplicationUser, User>().ForMember(a => a.UserId, b => b.MapFrom(a => a.Id)).ReverseMap();
            CreateMap<Customer, CustomerShort>().ReverseMap();
            CreateMap<Supplier,SupplierShort>().ReverseMap();
            CreateMap<AccountingMaster, AccountingMasterModel>().ReverseMap();
            CreateMap<AccountingDetail, AccountingDetailModel>().ReverseMap();
            CreateMap<Account,AccountCustomer>().ReverseMap();

            
        }
        public static string GetOperationType(OperationType operationType)
        {
            return operationType.ToString();
        }
        public static string GetInvoiceType(InvoiceType invoiceType)
        {
            return invoiceType.ToString();
        }

    }
}
