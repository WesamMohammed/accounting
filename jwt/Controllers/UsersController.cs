using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRolesPermissionsService _usersRolesPermissionsService;

        public UsersController(IUsersRolesPermissionsService usersRolesPermissionsService)
        {
            _usersRolesPermissionsService = usersRolesPermissionsService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInf()
        {

            var claim = this.User.Claims.FirstOrDefault(a=>a.Type=="uid");
          var userInfo=await _usersRolesPermissionsService.GetUserInf(claim.Value);

            return Ok(userInfo);


        }

        [HttpGet]
        [Authorize(AppPermissions.Users.View)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users= await _usersRolesPermissionsService.GetAllOtherUsers();

            return Ok(users);
        }
        [HttpGet]
        [Authorize(AppPermissions.Users.Edit)]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var UserRoles=await _usersRolesPermissionsService.GetUserRoles(userId);
            return Ok(UserRoles);
        }
        [HttpPost]
        [Authorize(AppPermissions.Users.Edit)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] ManageUserRolesViewModel model)
        {
            var result = await _usersRolesPermissionsService.UpdateUserRoles( model);
            return Ok(result);
        }
    }
}
