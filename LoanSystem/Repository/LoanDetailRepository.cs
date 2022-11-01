using LoanSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanSystem.Repository
{
    public class LoanDetailRepository : ILoanDetailRepository
    {
        private readonly LoanDbContext _loanDbContext;
        public LoanDetailRepository(LoanDbContext loanDbContext)
        {
            _loanDbContext = loanDbContext;
        }

        public List<LoanDetails> GetAll()
        {
            return _loanDbContext.LoanDetails.ToList(); 
        }
        public LoanDetails GetById(int id)
        {
            var loanDetail = _loanDbContext.LoanDetails.Find(id);
            if (loanDetail == null)
            {
                throw new Exception("LoanDetails Id not found ");
            }
            return loanDetail;
        }
        public IEnumerable<LoanDetailDTO> GetLoanandCustomerDetails(int id)
        {
            var loan = (from loans in _loanDbContext.Loan
                        join _loan in _loanDbContext.Loan on loans.LoanId equals _loan.LoanId
                        join customer in _loanDbContext.Customer on loans.CustomerId equals customer.CustomerId
                        join loanType in _loanDbContext.LoanType on loans.LoanTypeId equals loanType.LoanTypeId
                        join loanDetail in _loanDbContext.LoanDetails on loans.LoanId equals loanDetail.LoanId
                        where loans.LoanId == id
                        select new LoanDetailDTO()
                        {
                            LoanId = loanDetail.LoanId,
                            CustomerName = customer.CustomerName,
                            LoanName = loanType.LoanName,
                            PaidAmount = loanDetail.PaidAmount,
                            BalanceAmount = loanDetail.BalanceAmount,
                            BalanceDuration = loanDetail.BalanceDuration,
                            LoanAmount = _loan.LoanAmount,
                            DateOfSanction = _loan.DateOfSanction,
                            TotalPaidAmount = loanDetail.TotalPaidAmount
                        }).ToList();
            return loan;

        }


        public Messages PayLoan(LoanDetails loanDetails)
        {
            Messages message = new Messages();
            message.Success = false;
            var loan = _loanDbContext.Loan.Find(loanDetails.LoanId);
            if (loan == null)
            {
                message.Message = " Customer not taken any loan in this id";
                return message;
            }
            var payExists = _loanDbContext.LoanDetails.Where(x => x.LoanId == loanDetails.LoanId).ToList();
            var lastPay = payExists.LastOrDefault(x => x.LoanId == loan.LoanId);
            var duration = _loanDbContext.LoanType.FirstOrDefault(x => x.LoanTypeId == loan.LoanTypeId);
            if (loan.LoanAmount < loanDetails.PaidAmount)
            {
                message.Message = "Paid Amount should be less then loan amount";
            }
            if (lastPay == null)
            {

                loanDetails.BalanceAmount = loan.LoanAmount - loanDetails.PaidAmount;
                loanDetails.TotalPaidAmount = loanDetails.PaidAmount;
                loanDetails.BalanceDuration = duration.Duration - 1;
                _loanDbContext.Add(loanDetails);
                _loanDbContext.SaveChanges();
                message.Message = "Loan payed";
            }
            if (lastPay.BalanceAmount < loanDetails.PaidAmount)
            {
                message.Message = "Paid Amount should be less then loan amount";
            }
            if (lastPay != null)
            {
                loanDetails.BalanceAmount = lastPay.BalanceAmount - loanDetails.PaidAmount;
                loanDetails.TotalPaidAmount = lastPay.TotalPaidAmount + loanDetails.PaidAmount;
                loanDetails.BalanceDuration = lastPay.BalanceDuration - 1;
                _loanDbContext.Add(loanDetails);
                _loanDbContext.SaveChanges();
                message.Message = "Loan Payed for this customer";
            }
            return message;
        }

        public Messages UpdatePayedLoan(LoanDetails loanDetails)
        {
            Messages message = new Messages();
            message.Success = false;
            var loanDetail = _loanDbContext.LoanDetails.Find(loanDetails.LoanDetailsId);
            if (loanDetail == null)
            {
                message.Message = "LoanDetails Id not exists";
            }
            var balance = loanDetail.BalanceAmount + loanDetail.PaidAmount;
            var balanceAmount = balance - loanDetails.PaidAmount;
            var totalPaidAmount = loanDetail.TotalPaidAmount - loanDetail.PaidAmount;
            if (balanceAmount < 0)
            {
                message.Message = "Balance amount is not zero ";
            }
            if (balanceAmount >= 0)
            {
                loanDetail.PaidAmount = loanDetails.PaidAmount;
                loanDetail.BalanceAmount = balanceAmount;
                loanDetail.TotalPaidAmount = totalPaidAmount + loanDetails.PaidAmount;
                _loanDbContext.SaveChanges();
                message.Message = "Changes modified";
            }
            var loan = _loanDbContext.LoanDetails.FirstOrDefault(x => x.LoanDetailsId == loanDetails.LoanDetailsId);
            return message;
        }

        public Messages LoanClosed(int id)
        {
            Messages message = new Messages();
            message.Success = false;
            //var loanDetail = _loanDbContext.LoanDetails.Find(loanDetails.LoanDetailsId);
            var loan = _loanDbContext.Loan.Find(id);
            var loanDetails = _loanDbContext.LoanDetails.Where(x => x.LoanId == id).ToList();
            if (loan == null)
            {
                message.Message = " Loan  Id  is notfound";
                return message;
            }
            var last = loanDetails.LastOrDefault(x => x.LoanId == id);
            if (last == null)
            {
                message.Message = " This Id is not paid any Loan Amount";
                return message;
            }
            if (last.BalanceAmount == 0)
            {
                message.Success = true;
                message.Message = " Your Loan is completed Please check your LoanDetails";
                return message;
            }
            else
            {
                message.Message = " Your Loan is Not completed !!!!";
                return message;
            }


        }
    }
}
