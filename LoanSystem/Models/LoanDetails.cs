using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanSystem.Models
{

    public class LoanDetails
    {
        [Key]
        public int LoanDetailsId { get; set; }

        [ForeignKey("Loan")]
        public int LoanId { get; set; }

        [Required]
        public int PaidAmount { get; set; }
        public int TotalPaidAmount { get; set; }
        public int BalanceAmount { get; set; }
        public int BalanceDuration { get; set; }

    }
}
