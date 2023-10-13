using Microsoft.AspNetCore.Authorization;

namespace jwt.PermissionHandler
{
    public class PermissionRequirement:IAuthorizationRequirement
    {
        public string permission { get;private set; }
        public PermissionRequirement(string permission)
        {
            this.permission = permission;   
        }

    }
}
