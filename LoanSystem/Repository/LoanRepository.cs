using LoanSystem.Models;
using LoanSystem.Models.DTO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.WebSockets;

namespace LoanSystem.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _loanDbContext;
        public LoanRepository(LoanDbContext loanDbContext)
        {
            _loanDbContext = loanDbContext;
        }

        public List<Loan> GetLoans()
        {
            return _loanDbContext.Loan.ToList();
        }
        public Loan GetById(int id)
        {
            return _loanDbContext.Loan.Find(id);
        }
        
        public LoanDetailDTO GetLoanById(int id)
        {
            LoanDetailDTO loanDetail = new LoanDetailDTO();
            var loan =  _loanDbContext.Loan.Find(loanDetail.CustomerId);
            if(loan != null)
            {
                loanDetail.LoanId = loan.LoanId;
                loanDetail.LoanAmount = loan.LoanAmount;
                loanDetail.CustomerId = loan.CustomerId;
                loanDetail.LoanTypeId = loan.LoanTypeId;
                loanDetail.DateOfSanction = loan.DateOfSanction;
            }
            return loanDetail;
        }

        public Messages AddLoan(LoanDetailDTO loanDetail)
        {
            Loan loan = new Loan();
            loan.LoanId = loanDetail.LoanId;
            loan.CustomerId = loanDetail.CustomerId;
            loan.LoanTypeId = loanDetail.LoanTypeId;
            loan.DateOfSanction = loanDetail.DateOfSanction;
            loan.LoanAmount = loanDetail.LoanAmount;
            Messages message = new Messages();
            message.Success = false;
            var loantype = GetLoanTypeById(loan.LoanTypeId);
            if (loantype == null)
            {
                message.Success = false;
                message.Message = "LoanTypeId not exists";
                return message;
            }
            var customer = GetByCustomerId(loan.CustomerId);
            if (customer == null)
            {
                message.Success = false;
                message.Message = "CustomerId not exists";
                return message;
            }
            var _loan = _loanDbContext.Loan.Find(loanDetail.CustomerId);
            if (_loan != null)
            {
                _loanDbContext.Add(loan);
                _loanDbContext.SaveChanges();
                message.Success = true;
                message.Message = "Loan added successfully";
                return message;
            }
            else
            {
                message.Success = false;
                message.Message = "Customer already taken loan";
                return message;
            }
        }

        public Messages UpdateLoan(LoanDetailDTO loanDetail)
        {
            Loan loan = new Loan();
            loan.LoanId = loanDetail.LoanId;
            loan.CustomerId = loanDetail.CustomerId;
            loan.LoanTypeId=loanDetail.LoanTypeId;
            loan.DateOfSanction = loanDetail.DateOfSanction;
            loan.LoanAmount = loanDetail.LoanAmount;
            Messages message = new Messages();
            message.Success = false;
            var _loan = _loanDbContext.Loan.Where(x => x.LoanId == loan.LoanId).FirstOrDefault();
            var detail = _loanDbContext.LoanDetails.Where(x => x.LoanId == loan.LoanId).FirstOrDefault();
            if (detail == null)
            {
                _loan.DateOfSanction = loan.DateOfSanction;
                _loan.LoanAmount = loan.LoanAmount;
                _loanDbContext.SaveChanges();
                message.Message = "Loan updated";
                return message;
            }
            else
            {
                message.Message = "Loan can't be updated because customer payed loan amount";
            }
            var loanValue = _loanDbContext.Loan.FirstOrDefault(x => x.LoanId == loan.LoanId);
            return message;
        }

        public LoanType GetLoanTypeById(int id)
        {
            return _loanDbContext.LoanType.Find(id);
        }

        public Customer GetByCustomerId(int id)
        {
            return _loanDbContext.Customer.Find(id);
        }

        public IEnumerable<LoanDetailDTO> LoanList()
        {
            var loan = (from loans in _loanDbContext.Loan
                        join _loan in _loanDbContext.Loan on loans.LoanId equals _loan.LoanId
                        join customer in _loanDbContext.Customer on loans.CustomerId equals customer.CustomerId
                        join loanType in _loanDbContext.LoanType on loans.LoanTypeId equals loanType.LoanTypeId
                        //join loanDetail in _loanDbContext.LoanDetails on loans.LoanId equals loanDetail.LoanId
                        //where loans.LoanId == id
                        select new LoanDetailDTO()
                        {
                            LoanId = _loan.LoanId,
                            CustomerName = customer.CustomerName,
                            LoanName = loanType.LoanName,
                            //PaidAmount = loanDetail.PaidAmount,
                            //BalanceAmount = loanDetail.BalanceAmount,
                            //BalanceDuration = loanDetail.BalanceDuration,
                            LoanAmount = _loan.LoanAmount,
                            DateOfSanction = _loan.DateOfSanction
                        }).ToList();
            return loan;
        }

        public IEnumerable<CustomerDTO> GetLoanByCustomerId(int customerId)
        {
            var customers = (from loan in _loanDbContext.Loan
                             //join loans in _loanDbContext.Loan on loan.LoanId equals loans.LoanId
                             join customer in _loanDbContext.Customer on loan.CustomerId equals customer.CustomerId
                             join loanType in _loanDbContext.LoanType on loan.LoanTypeId equals loanType.LoanTypeId
                             where loan.CustomerId == customerId
                             select new CustomerDTO
                             {
                                 CustomerId = customerId,
                                 CustomerName = customer.CustomerName,
                                 LoanName = loanType.LoanName,
                                 LoanAmount = loan.LoanAmount,
                                 DateOfSanction = loan.DateOfSanction
                             }).ToList();
            return customers;
        }

        //for loan 
        public CustomerDTO GetCustomer(int customerId)
        {
            var loans = (from customer in _loanDbContext.Customer
                         join loan in _loanDbContext.Loan on customer.CustomerId equals loan.CustomerId
                         join loanType in _loanDbContext.LoanType on loan.LoanTypeId equals loanType.LoanTypeId
                         where customer.CustomerId == customerId
                         select new CustomerDTO
                         {
                             CustomerId = customerId,
                             CustomerName = customer.CustomerName,
                             LoanName = loanType.LoanName,
                             LoanId = loan.LoanId,
                             LoanAmount = loan.LoanAmount,
                             DateOfSanction = loan.DateOfSanction
                         }).FirstOrDefault();
            return loans;
        }
        public LoanDetailDTO GetLoan(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
