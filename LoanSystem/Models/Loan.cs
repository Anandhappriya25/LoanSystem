using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanSystem.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Display(Name = "LoanType")]
        public virtual int LoanTypeId { get; set; }

        [ForeignKey("LoanTypeId")]
        public virtual LoanType LoanTypes { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Please enter the loan sanction date")]
        public DateTime DateOfSanction { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Loan Amount")]
        public int LoanAmount { get; set; }

    }
}
