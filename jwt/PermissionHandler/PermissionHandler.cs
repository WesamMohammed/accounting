using Microsoft.AspNetCore.Authorization;

namespace jwt.PermissionHandler
{
    public class PermissionHandler:AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return ;
            }
            var UserPermission=context.User.Claims.Where(a=>a.Type== "Permissions" && a.Value==requirement.permission);
            if (UserPermission.Any())
            {
                context.Succeed(requirement);
                return;
            }

        }
    }
}
