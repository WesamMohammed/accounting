using jwt.Models;

namespace jwt.Services
{
    public interface IProductService
    {
        Task<MyAction> AddProductAsync(ProductModel applicationModel);
        Task<ProductModel> GetProductByNameAsync(string Name);
        Task<ProductModel> GetProductByIdAsync(int Id);
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> UpdateProduct(ProductModel productModel);
        
    }
}
