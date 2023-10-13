using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StoreController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddStore(Store store)
        {
            await _applicationDbContext.Stores.AddAsync(store);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(store);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _applicationDbContext.Stores.ToListAsync();
            return Ok(stores);
        }
    }
}
