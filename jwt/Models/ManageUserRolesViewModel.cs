namespace jwt.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<UserRolesViewModel> userRoles { get; set; }

    }
}
