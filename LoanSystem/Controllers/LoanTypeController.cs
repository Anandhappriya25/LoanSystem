using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTypeController : ControllerBase
    {
        private readonly ILoanTypeRepository _loanTypeRepository;
        public LoanTypeController(ILoanTypeRepository loanTypeRepository)
        {
            _loanTypeRepository = loanTypeRepository;
        }

        [HttpGet("GetAllLoanType")]
        public IActionResult GetAll()
        {
            var loanType = _loanTypeRepository.GetAll();
            if (loanType.Count() == 0)
            {
                return NotFound();
            }
            return Ok(loanType);
        }

        [HttpGet("GetByLoanTypeId/{id}")]
        public IActionResult GetByLoanTypeId(int id)
        {
            var loanType = _loanTypeRepository.GetById(id);
            if (loanType == null)
            {
                return NotFound();
            }
            return Ok(loanType);
        }



        [HttpPost("AddLoanType")]
        public IActionResult AddLoanType(LoanType loanType)
        {
            var _loanType = _loanTypeRepository.AddLoanType(loanType);
            if (_loanType == null)
            {
                return NotFound();
            }
            return Ok(loanType);
        }

        [HttpPut("UpdateLoanType/{id}")]
        public IActionResult UpdateLoanType(LoanType loanType)
        {
            var _loanType = _loanTypeRepository.UpdateLoanType(loanType);
            if (_loanType == null)
            {
                throw new Exception("The LoantypeId not exists");
            }
            return Ok(_loanType);
        }

        [HttpDelete("DeleteLoanType/{id}")]
        public IActionResult DeleteLoanType(int id)
        {
            return Ok(_loanTypeRepository.DeleteLoanType(id));
        }
    }
}
