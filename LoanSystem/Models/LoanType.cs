using System.ComponentModel.DataAnnotations;

namespace LoanSystem.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Loan Name can not be empty")]
        public string LoanName { get; set; }

        [Required(ErrorMessage = "Enter the months")]
        [Range(6, 24)]
        public int Duration { get; set; }
    }
}
