using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LoanSystem.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "EmailId")]
        public string EmailId { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        
        public bool RememberLogin { get; set; }
        
        public string ReturnUrl { get; set; }
        
    }
}
