using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoanSystem.Models.DTO
{
    public class DetailsDTO
    {
        public int LoanId { get; set; }
        public string CustomerName { get; set; }
        public string LoanName { get; set; }
        public DateTime DateOfSanction { get; set; }
        public int LoanAmount { get; set; }

        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> LoanTypes { get; set; }
    }
}