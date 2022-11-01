using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoanSystem.Models
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
        {

        }

        public DbSet<LoanType> LoanType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanDetails> LoanDetails { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}