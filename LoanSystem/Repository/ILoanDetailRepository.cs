using LoanSystem.Models;

namespace LoanSystem.Repository
{
    public interface ILoanDetailRepository
    {
        List<LoanDetails> GetAll();
        LoanDetails GetById(int id);
        Messages PayLoan(LoanDetails loanDetails);
        Messages UpdatePayedLoan(LoanDetails loanDetails);
        Messages LoanClosed(int id);
        IEnumerable<LoanDetailDTO> GetLoanandCustomerDetails(int id);
        List<LoanDetailDTO> LoanDetailsById(int id);

    }
}