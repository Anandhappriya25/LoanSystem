using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LoanSystem.Models
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public long AadharNumber { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int LoanId { get; set; }
        public int LoanTypeId { get; set; }
        public string LoanName { get; set; }
        public DateTime DateOfSanction { get; set; }
        public int LoanAmount { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public bool Role { get; set; }
    }
}
