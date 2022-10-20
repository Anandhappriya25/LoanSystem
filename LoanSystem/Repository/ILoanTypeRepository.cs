using LoanSystem.Models;

namespace LoanSystem.Repository
{
    public interface ILoanTypeRepository
    {
        List<LoanType> GetAll();
        LoanType GetById(int id);
        LoanType GetByName(string name);
        Messages AddLoanType(LoanType loanType);
        Messages UpdateLoanType(LoanType loanType);
        Messages DeleteLoanType(int id);
    }
}
