using System.ComponentModel.DataAnnotations;
namespace jwt.Models
{
    public class AddRoleToUserModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
