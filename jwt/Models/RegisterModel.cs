using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class RegisterModel
    {
        [Required, MaxLength(255)]
        public string FirstName { get; set; }
        [Required, MaxLength(255)]
        public string LastName { get; set; }
        [Required, MaxLength(255)]
        public string UserName { get; set; }
        [Required, MaxLength(255)]
        public string Email { get; set; }
        [Required, MaxLength(255)]
        public string Password { get; set; }
        
        

    }
}
