using LoanSystem.Models;

namespace LoanSystem.Models
{
    public class Messages
    {
        public bool Success { get; set; }
        public string Message { get; set; } = String.Empty;
        public bool Role { get; set; }
    }
}