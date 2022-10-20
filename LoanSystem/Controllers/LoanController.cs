using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace LoanSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;
        public LoanController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpGet("GetAllLoans")]
        public IActionResult GetAllLoans()
        {
            var _loan = _loanRepository.GetLoans();
            if (_loan.Count() == 0)
            {
                throw new Exception("No values in the Loan database");
            }
            return Ok(_loan);
        }

        [HttpGet("GetLoanById/{id}")]
        public IActionResult GetByLoanId(int id)
        {
            var loan = _loanRepository.GetLoanById(id);
            if (loan == null)
            {
                throw new Exception("The LoanId not exists");
            }
            return Ok(loan);
        }


        [HttpPost("AddLoan")]
        public IActionResult AddLoan(LoanDetailDTO loanDetail)
        {
            var _loan = _loanRepository.AddLoan(loanDetail);
            if (_loan == null)
            {
                throw new Exception("Already Exists");
            }
            return Ok(_loan);
        }

        [HttpPut("UpdateLoan")]
        public IActionResult UpdateLoan(LoanDetailDTO loanDetail)
        {
            var _loan = _loanRepository.UpdateLoan(loanDetail);
            if (_loan == null)
            {
                throw new Exception("The LoanId not exixsts");
            }
            return Ok(_loan);
        }
    }
}
