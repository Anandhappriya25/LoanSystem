using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("GetAllCustomer")]

        public IActionResult GetAll()
        {
            var customer = _customerRepository.GetAll();
            if (customer.Count() == 0)
            {
                throw new Exception("No Details in the customer table");
            }
            return Ok(customer);
        }

        [HttpGet("GetByCustomerId/{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var customer = _customerRepository.GetByName(name);
            if (customer == null)
            {
                throw new Exception("CustomerName not found in the list");
            }
            return Ok(customer);
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(CustomerDTO customer)
        {
            var customers = _customerRepository.AddCustomer(customer);
            return Ok(customer);
        }

        [HttpPut("UpdateCustomer/{id}")]
        public IActionResult UpdateCustomer(CustomerDTO customers)
        {
            var customer = _customerRepository.UpdateCustomer(customers);
            if (customer == null)
            {
                throw new Exception("The customerid not exists");
            }
            return Ok(customer);
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            return Ok(_customerRepository.DeleteCustomer(id));
        }
    }
}

