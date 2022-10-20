using LoanSystem.Models;

namespace LoanSystem.Repository
{
    public class LoanTypeRepository : ILoanTypeRepository
    {

        private readonly LoanDbContext _loanDbContext;
        public LoanTypeRepository(LoanDbContext loanDbContext)
        {
            _loanDbContext = loanDbContext;
        }
        public LoanType GetByName(string name)
        {
            return _loanDbContext.LoanType.FirstOrDefault(x => x.LoanName == name);
        }
        public Messages AddLoanType(LoanType loanType)
        {
            Messages message = new Messages();
            message.Success = false;
            var _loanType = GetByName(loanType.LoanName);
            if (_loanType != null)
            {
                message.Message = "LoanType name already exists";
                return message;
            }
            else
            {
                _loanDbContext.LoanType.Add(loanType);
                _loanDbContext.SaveChanges();
                message.Success = true;
                message.Message = "LoanType added successfully";
                return message;
            }
        }
        public List<LoanType> GetAll()
        {
            return _loanDbContext.LoanType.ToList();
        }

        public LoanType GetById(int id)
        {
            return _loanDbContext.LoanType.Find(id);
        }

        public Messages UpdateLoanType(LoanType loanType)
        {
            Messages message = new Messages();
            message.Success = false;
            var _loanType = _loanDbContext.LoanType.Where(x => x.LoanTypeId == loanType.LoanTypeId).FirstOrDefault();
            if (_loanType != null)
            {
                _loanType.LoanName = loanType.LoanName;
                _loanType.Duration = loanType.Duration;
                _loanDbContext.SaveChanges();
                message.Success = true;
                message.Message = "LoanType updated successfully";
                return message;
            }
            else
            {
                message.Message = "LoanType not updated ";
                return message;
            }

        }

        public Messages DeleteLoanType(int id)
        {
            Messages message = new Messages();
            message.Success = false;
            var loanType = _loanDbContext.LoanType.Find(id);
            var loan = _loanDbContext.Loan.Find(id);
            if (loan != null)
            {
                message.Message = "You can't remove loantype because they didn't complete the loan ";
                return message;
            }
            else
            {
                _loanDbContext.LoanType.Remove(loanType);
                _loanDbContext.SaveChanges();
                message.Message = "LoanType removed";
                return message;
            }
        }

    }
}

