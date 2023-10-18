using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using jwt.Services;
using jwt.Models;
using Microsoft.AspNetCore.Authorization;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpPost]
        [Authorize(AppPermissions.products.Create)]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.AddProductAsync(productModel);
            if (!result.Succeeded) {

                return BadRequest(result.Message);
            
            }

            return Ok(result);
        }
        [HttpGet]
        [Authorize(AppPermissions.products.View)]
        public async Task<IActionResult> GetProductByName(string Name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _productService.GetProductByNameAsync(Name);
            if (result is null)
            {

                return BadRequest("this Product is Not Exist");

            }

            return Ok(result);
        }
        [HttpGet]
       [Authorize(AppPermissions.products.View)]
        public async Task<IActionResult> GetProductById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _productService.GetProductByIdAsync(Id);
            if (result is null)
            {

                return BadRequest("this Product is Not Exist");

            }

            return Ok(result);
        }
        [HttpGet]
        [Authorize(AppPermissions.products.View)]
        public async Task<IActionResult> GetAllProducts()
        {
           


            var result = await _productService.GetAllProductsAsync();
            if (result is null)
            {

                return BadRequest("There is No Products Exist");

            }

            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProduct(ProductModel productModel){

             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(productModel.Id==0){
                return NotFound();
            }
            var result=await _productService.UpdateProduct(productModel);

            if(result is null){
                return NotFound();
            }
            if(result.Id==0){
                return BadRequest(new{errorMessage=result.ProductUnits.First().UnitName+ "  can not be deleted it has movement"});
            }
            return Ok(result);
        }

    }
}
