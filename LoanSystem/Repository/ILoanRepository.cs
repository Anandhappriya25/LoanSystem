using LoanSystem.Models;

namespace LoanSystem.Repository
{
    public interface ILoanRepository
    {
        List<Loan> GetLoans();
        Loan GetById(int id);
        LoanType GetLoanTypeById(int id);
        Customer GetByCustomerId(int id);
        LoanDetailDTO GetLoanById(int id);
        Messages AddLoan(LoanDetailDTO loanDetail);
        Messages UpdateLoan(LoanDetailDTO loanDetail);
        IEnumerable<LoanDetailDTO> LoanList();
        IEnumerable<CustomerDTO> GetLoanByCustomerId(int customerId);
        CustomerDTO GetCustomer(int customerId);
        LoanDetailDTO GetLoan(int customerId);
    }
}