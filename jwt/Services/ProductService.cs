using jwt.Models;
using jwt.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace jwt.Services
{
    public class ProductService:IProductService

    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductService(ApplicationDbContext applicationDbContext,IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }


        public async Task<MyAction> AddProductAsync(ProductModel productModel)
        {
           
            var myAtion = new MyAction();
            
            var productExist = await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.Name == productModel.Name);
            if (productExist is not null)
            {
                myAtion.Message = "this Product Already Exist";
                return myAtion;
            }
            var units = new List<ProductUnit>();
            
            foreach (var unit in productModel.ProductUnits)
            {
                if((unit.Index != 1 && unit.Ratio==1)|| unit.Ratio==0)
                {
                    myAtion.Message = $"ratio of unit {unit.UnitName} can not be {unit.Ratio} ";
                    
                }
                
                units.Add(new ProductUnit() { 
                    UnitName = unit.UnitName,
                    UnitPurchasingPrice = unit.UnitPurchasingPrice,
                    UnitBarCode=unit.UnitBarCode,
                    UnitSellingPrice=unit.UnitSellingPrice,
                    Ratio=unit.Ratio,
                    Index=unit.Index
                }); 
            }

            var product = new Product()
            {
                Name=productModel.Name,
                CategoryId=productModel.CategoryId,
                PurchasingPrice=productModel.PurchasingPrice,
                SellingPrice=productModel.SellingPrice,
                Description=productModel.Description,
                Barcode=productModel.Barcode,
                ProductUnits=units,


            };


            await _applicationDbContext.Products.AddAsync(product);

            var result = _applicationDbContext.SaveChanges();


            if (!(result > 0))
            {
                
                myAtion.Message = "Something went wrong";
                return myAtion;

            }

            myAtion.Message = $"Product {product.Name} Added Succefuly";
            myAtion.Succeeded = true;

            return myAtion;
        }

        public async Task<ProductModel> GetProductByNameAsync(string Name)
        {
            var product= await _applicationDbContext.Products.Include(x=>x.ProductUnits).FirstOrDefaultAsync(x=>x.Name==Name);
            var productModel = _mapper.Map<ProductModel>(product);
            if (product == null)
            {
               
                return null;

            }
           
            return productModel;



        }
        public async Task<ProductModel> GetProductByIdAsync(int Id)
        {
            var product = await _applicationDbContext.Products.Include(x => x.ProductUnits).FirstOrDefaultAsync(x => x.Id == Id);
            var productModel = _mapper.Map<ProductModel>(product);
            if (product == null)
            {

                return null;

            }

            return productModel;



        }
        public async Task<ProductModel> UpdateProduct(ProductModel productModel)
        {
            var product = await _applicationDbContext.Products.Include(x => x.ProductUnits).FirstOrDefaultAsync(x => x.Id == productModel.Id);
            if(product is null){
                return null;
            }
            product.Name=productModel.Name;
            product.Description=productModel.Description;
            var ProductUnitsToDelet=new List<ProductUnit>();
            foreach (var unit in product.ProductUnits){
                var productUnit= productModel.ProductUnits.FirstOrDefault(a=>a.UnitId==unit.UnitId);
                if(productUnit is null){
                    ProductUnitsToDelet.Add(unit);
                }
                else{
                    unit.UnitName=productUnit.UnitName;
                    unit.Ratio=productUnit.Ratio;
                    unit.UnitPurchasingPrice=productUnit.UnitPurchasingPrice;
                    unit.UnitSellingPrice=productUnit.UnitSellingPrice;

                }

            }
            foreach(var unit in ProductUnitsToDelet){
                if( !_applicationDbContext.InvoiceDetails.Any(a=>a.ProductUnitId==unit.UnitId)){
                    product.ProductUnits.Remove(unit);
                }
                var ProductUnitNoDelete=new ProductModel{Name=productModel.Name,ProductUnits=new List<ProductUnitModel>{_mapper.Map<ProductUnitModel>(unit)}};
                return ProductUnitNoDelete;
            }
            foreach(var unit in productModel.ProductUnits){

                if(  unit.UnitId==0){
                    product.ProductUnits.Add(_mapper.Map<ProductUnit>(unit));

                }
                _applicationDbContext.SaveChanges();

            }
            return _mapper.Map<ProductModel>(product);
            
            


        }
        public async Task<List<ProductModel>> GetAllProductsAsync() {

            var products = await _applicationDbContext.Products.Include(x => x.ProductUnits).ToListAsync();

            var productsModel = _mapper.Map<List<ProductModel>>(products);
            if(products is null)
            {
                return null;
            }
            return productsModel;
        }

    }
}
