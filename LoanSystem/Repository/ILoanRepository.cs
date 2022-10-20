using LoanSystem.Models;

namespace LoanSystem.Repository
{
    public interface ILoanRepository
    {
        List<Loan> GetLoans();
        LoanType GetLoanTypeById(int id);
        Customer GetById(int id);
        Loan GetLoanById(int id);
        Messages AddLoan(LoanDetailDTO loanDetail);
        Messages UpdateLoan(LoanDetailDTO loanDetail);
        IEnumerable<LoanDetailDTO> LoanList();
    }
}