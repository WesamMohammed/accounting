using System.Text.Json.Serialization;
namespace jwt.Models
{
    public class AccountOutPutModel:AccountModel
    {
 


        public ICollection<AccountOutPutModel> children { get; set; }

    }
}
