using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using jwt.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using jwt.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
       private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService )
        {
            _accountService = accountService;
            
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAccounts()
        {
           var result=await _accountService.GetAllAccounts();
            if(result is null){
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubAccounts(InvoiceType? invoiceType)
        {
            
            var result=await _accountService.GetSubAccounts(invoiceType);
            if(result is null){
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]

        public async Task<IActionResult> AddAcount( AccountModel accountModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        var result= await _accountService.AddAccountAsync(accountModel);

        if(result is null){
            return BadRequest();
        }
            return Ok(result);
        }



    }
}
