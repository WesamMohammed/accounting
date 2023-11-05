using AutoMapper;
using jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCustomers(CustomerModel? customerModel)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var customers = await _customerService.GetAllCustomers(customerModel);
            return Ok(customers);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllSupplires()
        {
            var supplires = await _customerService.GetAllSuppliersAsync();
            return Ok(supplires);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddCustomer(CustomerModel customerModel) {

            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var result = await _customerService.AddCustomer(customerModel);
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMainAccountsCustomerType() {


            var result = await _customerService.GetMainAccountsCustomerType();
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMainAccountsSuppliers()
        {
            var result = await _customerService.GetMainAccountsSupplierTyse();
            return Ok(result);

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customerModel) {

            var result = await _customerService.UpdateCustomer(customerModel);
            if (result is null) {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var result = await _customerService.GetById(id, AccountType.Customer);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSupplierById([FromRoute] int id)
        {
            var result = await _customerService.GetById(id,AccountType.Supplier);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
