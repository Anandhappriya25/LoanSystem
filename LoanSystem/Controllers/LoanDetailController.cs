using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanDetailController : ControllerBase
    {
        private readonly ILoanDetailRepository _loanDetailRepository;
        public LoanDetailController(ILoanDetailRepository loanDetailRepository)
        {
            _loanDetailRepository = loanDetailRepository;
        }

        [HttpGet("GetAllLoanDetails")]
        public IActionResult GetAllLoanDetail()
        {
            var loanDetail = _loanDetailRepository.GetAll();
            if (loanDetail.Count() == 0)
            {
                return NotFound();
            }
            return Ok(loanDetail);
        }

        [HttpGet("GetLoanDetailById/{id}")]
        public IActionResult GetLoanDetailById(int id)
        {
            var loanDetail = _loanDetailRepository.GetById(id);
            if (loanDetail == null)
            {
                throw new Exception("LoanDetailId not found in lists ");
            }
            return Ok(loanDetail);
        }


        [HttpPost("PayLoan")]
        public IActionResult PayLoan(LoanDetails loanDetails)
        {
            return Ok(_loanDetailRepository.PayLoan(loanDetails));
        }

        [HttpPut("UpdatePayedLoan")]
        public IActionResult UpdatePayedLoan(LoanDetails loanDetails)
        {
            return Ok(_loanDetailRepository.UpdatePayedLoan(loanDetails));
        }

        [HttpPut("CloseLoan/{id}")]
        public IActionResult LoanClosed(int id)
        {
            return Ok(_loanDetailRepository.LoanClosed(id));
        }

        [HttpGet("GetLoanandCustomerDetails/{id}")]
        public IActionResult GetLoanandCustomerDetails(int id)
        {
            return Ok(_loanDetailRepository.GetLoanandCustomerDetails(id));
        }
    }
}