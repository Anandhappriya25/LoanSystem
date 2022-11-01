using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Loan Name can not be empty")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Loan Name field must have minimum 5 and maximum 15 character!")]
        public string CustomerName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact number is required ")]
        [RegularExpression("([0-9]{10})", ErrorMessage = "Mobile number must be 10 digits")]
        public string MobileNumber { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "The field can't be empty")]
        [RegularExpression("([0-9]{12})", ErrorMessage = "Aadhar Number must be 12 digits")]
        public long AadharNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string EmailId { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}