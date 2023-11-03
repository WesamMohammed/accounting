using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using jwt.Services;
using jwt.Entities;
using jwt.Models;
using Microsoft.AspNetCore.Authorization;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _salesInvoiceService;

        public SalesInvoiceController(ISalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }
        [HttpPost]

        public async Task<IActionResult> AddSales([FromBody] SalesInvoiceModel salesInvoice)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            salesInvoice.AccountDainId = 5;
            salesInvoice.OperationType = OperationType.SalesInvoice;
            var result=await _salesInvoiceService.AddSalesInvoiceAsync(salesInvoice);


          return Ok(result);
        }
        [HttpPost]

        public async Task<IActionResult>AddReturnSales(SalesInvoiceModel salesInvoice)
        {
            salesInvoice.OperationType = OperationType.ReturnSalesInvoice;
            
               var result = await _salesInvoiceService.AddReturneSalesAsync(salesInvoice);
            return Ok(result);
            
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSalesInvoice(SalesInvoiceModel? salesInvoiceModel)
        {


            
            var result = await _salesInvoiceService.GetAllSalesInvoiceAsync(OperationType.SalesInvoice,salesInvoiceModel);
         
        //    result.ForEach(a => { a.AccountDain = null; a.AccountDainId = 0; });

            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSalesPagination(SalesInvoiceModel? salesInvoiceModel ,int page=1,int pageSize=10,string sort="Id",bool isAscending=true)
        {
          
           var result =await  _salesInvoiceService.GetAllSalesInvoiceAsync(OperationType.SalesInvoice,salesInvoiceModel,page,pageSize,sort,isAscending);
           
            if (result is null)
            {

                return BadRequest("There is No Sales Exist");

            }
        //    result.ForEach(a => { a.AccountDain = null; a.AccountDainId = 0; });

            return Ok(result);
            
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllReturnSalesPagenation(SalesInvoiceModel? salesInvoiceModel ,int page=1,int pageSize=10,string sort="Id",bool isAscending=true)
        {



            var result = await  _salesInvoiceService.GetAllSalesInvoiceAsync(OperationType.ReturnSalesInvoice,salesInvoiceModel,page,pageSize,sort,isAscending);
            if (result is null)
            {

                return BadRequest("There is No Sales Exist");

            }
            result.ForEach(a => { a.AccountMadin = null; a.AccountMadinId = 0; });
            return Ok(result);
             
        }
              [HttpPost]
        [AllowAnonymous]
            public async Task<IActionResult> GetAllReturnSales(SalesInvoiceModel? salesInvoiceModel)
        {



            var result = await _salesInvoiceService.GetAllSalesInvoiceAsync(OperationType.ReturnSalesInvoice,salesInvoiceModel);
            if (result is null)
            {

                return BadRequest("There is No Sales Exist");

            }
            result.ForEach(a => { a.AccountMadin = null; a.AccountMadinId = 0; });
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

            var result = await _salesInvoiceService.GetSalesInvoiceByIdAsync(id,OperationType.SalesInvoice);
            if (result is null)
            {

                return BadRequest($"There is No Sale invoice by {id} number ");

            }

            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReturnSalesByid(int? id)
        {

            if (id == null)
            {
                return BadRequest("Enter the Query Id");
            }

            var result = await _salesInvoiceService.GetSalesInvoiceByIdAsync(id, OperationType.ReturnSalesInvoice);
            if (result is null)
            {

                return BadRequest($"There is No Return Sale  by {id} number ");

            }

            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>UpdateSalesInvoice(SalesInvoiceModel salesInvoice)
        {
            if(salesInvoice is null)
            {
                return BadRequest();
            }
            salesInvoice.AccountDainId = 5;

            var result = await _salesInvoiceService.UpdateSalesInvoiceAsync(salesInvoice);
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
            
        }
	 [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateReturnSales(SalesInvoiceModel salesInvoice)
        {
            if (salesInvoice is null)
            {
                return BadRequest();
            }
            var result = await _salesInvoiceService.UpdateReturnSalesAsync(salesInvoice);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);

        }
    }
}
