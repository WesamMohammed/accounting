using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesInvoiceService _purchasesInvoice;

        public PurchasesController(IPurchasesInvoiceService purchasesInvoice)
        {
            _purchasesInvoice = purchasesInvoice;
        }
        [HttpPost]
        [Authorize(AppPermissions.Purchases.Create)]
        public async Task<IActionResult> AddPurchases([FromBody] PurchasesInvoiceModel purchasesInvoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            purchasesInvoice.AccountMadinId = 16;//حساب المخزون السلعي 
            var result = await _purchasesInvoice.AddPurchasesInvoiceAsync(purchasesInvoice);


            return Ok(result);
        }

         [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPurchasesInvoice(SalesInvoiceModel? salesInvoiceModel)
        {


            
            var result = await _purchasesInvoice.GetAllSalesInvoiceAsync(OperationType.PurchasesInvoice,salesInvoiceModel);
            if (result is null)
            {

                return BadRequest("There is No Sales Exist");

            }
          //  result.ForEach(a => { a.AccountDain = null; a.AccountDainId = 0; });

            return Ok(result);
        }
         [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalesInvoiceByid(int? id)
        {

            if(id == null)
            {
                return BadRequest("Enter the Query Id");
            }

            var result = await _purchasesInvoice.GetSalesInvoiceByIdAsync(id,OperationType.PurchasesInvoice);
            if (result is null)
            {

                return BadRequest($"There is No Sale invoice by {id} number ");

            }

            return Ok(result);
        }

         [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>UpdateSalesInvoice(PurchasesInvoiceModel salesInvoice)
        {
            if(salesInvoice is null)
            {
                return BadRequest();
            }
            salesInvoice.AccountMadinId = 16;//حساب المخزون السلعي 
            var result = await _purchasesInvoice.UpdateSalesInvoiceAsync(salesInvoice);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
            
        }
        
    }
}
