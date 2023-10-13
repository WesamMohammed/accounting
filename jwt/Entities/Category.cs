using System.ComponentModel.DataAnnotations;

namespace jwt.Entities
{
    public class Category:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
