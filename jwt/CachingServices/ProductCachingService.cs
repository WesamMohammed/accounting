using jwt.DistributedCachExtension;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace jwt.CachingServices
{
    public class ProductCachingService : IProductService
    {
        private readonly ProductService _productService;
        private readonly IDistributedCache _distributedCache ;

        public ProductCachingService(ProductService productService, IDistributedCache distributedCache)
        {
            _productService = productService;
            _distributedCache = distributedCache;
        }

        public async Task<MyAction> AddProductAsync(ProductModel applicationModel)
        {
            return await _productService.AddProductAsync(applicationModel);
        }

        public  async Task<List<ProductModel>> GetAllProductsAsync() 
        {
            string Key = "Productss";

            var result = await _distributedCache.GetOrCreateCache(Key, async () =>
            {
                return await _productService.GetAllProductsAsync();
            });
            return result;


        }

        public async Task<ProductModel> GetProductByIdAsync(int Id)
        {
            string Key = $"Products-{Id}";
            return await _productService.GetProductByIdAsync(Id);
        }

        public async Task<ProductModel> GetProductByNameAsync(string Name)
        {
            return await _productService.GetProductByNameAsync(Name);
        }

        public async Task<ProductModel> UpdateProduct(ProductModel productModel) 
        {
            return await _productService.UpdateProduct(productModel);
        }
    }
}
