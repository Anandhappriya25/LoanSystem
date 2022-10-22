using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoanSystem.Models
{
    public class LoanDetailDTO
    {
        public int LoanId { get; set; }
        public int LoanTypeId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int MobileNumber { get;set; }
        public string LoanName { get; set; }
        public DateTime DateOfSanction { get; set; }
        public int LoanAmount { get; set; }
        public int PaidAmount { get; set; }
        public int TotalPaidAmount { get; set; }
        public int BalanceAmount { get; set; }
        public int BalanceDuration { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> LoanTypes { get; set; }
    }
}
