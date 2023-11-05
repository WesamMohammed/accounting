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
        [HttpGet]
        public async Task<IActionResult>GetById(int id)
        {
            var store =  _applicationDbContext.Stores.Find(id);
            if(store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Store store)
        {
            var id = store.Id;
            var storeEx = _applicationDbContext.Stores.Find(id);
            if (storeEx == null)
            {
                return NotFound();
            }
            _applicationDbContext.Update(store);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(store);
        }
    }
}
