using jwt.Models;
using jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrAsync(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =await _authService.RegisterAsync(registerModel);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginModel);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }


            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleToUserModel addRoleToUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.AddRoleToUser(addRoleToUserModel);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }


            return Ok("Done");
        }
    }
}
