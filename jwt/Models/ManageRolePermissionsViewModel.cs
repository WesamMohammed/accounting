namespace jwt.Models
{
    public class ManageRolePermissionsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionsViewModel>RolePermissions {get;set;}
    }
}
