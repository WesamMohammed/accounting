using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   // [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IUsersRolesPermissionsService _usersRolesPermissionsService;

        public RolesController(IUsersRolesPermissionsService usersRolesPermissionsService)
        {
            _usersRolesPermissionsService = usersRolesPermissionsService;
        }
        [HttpGet]
      //  [Authorize(AppPermissions.Roles.View)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles =await _usersRolesPermissionsService.GetAllRoles();
            return Ok(roles);
        }
        [HttpGet]
      //  [Authorize(AppPermissions.Roles.Edit)]
        public async Task<IActionResult> GetRolePermissions(string? RoleId)
        {
            var permissions = await _usersRolesPermissionsService.GetRolePermissions(RoleId);
            return Ok(permissions);
        }
        [HttpPost]
       // [Authorize(AppPermissions.Roles.Create)]
        public async Task<IActionResult> AddRole(ManageRolePermissionsViewModel model)
        {
            var role = await _usersRolesPermissionsService.AddRole(model);
            return Ok(role);
        }

        [HttpPost]
     //   [Authorize(AppPermissions.Roles.Edit)]
        public async Task<IActionResult> UpdateRolePermissions(ManageRolePermissionsViewModel model)
        {
            var result = await _usersRolesPermissionsService.UpdateRolePermissions(model);
            return Ok(result);
        }

    }
}
