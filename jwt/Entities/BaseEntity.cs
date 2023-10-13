using jwt.Models;

namespace jwt.Entities
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } 

        public string? ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }



    }
}
