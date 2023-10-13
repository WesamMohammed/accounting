using jwt.Entities;
using System.ComponentModel.DataAnnotations;

namespace jwt.Models
{
    public class AccountModel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public int accountNumber { get; set; }
        [Required]
        
        public bool isSub { get; set; }
        [Required]
        public int parentId { get; set; }
        public AppearIn appearIn { get; set; }
        public AccountType accountType { get; set; }




    }
}
