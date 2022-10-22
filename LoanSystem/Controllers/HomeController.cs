using LoanSystem.Models;
using LoanSystem.Models.DTO;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace LoanSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanDetailRepository _loanDetailRepository;

        public HomeController(ILogger<HomeController> logger, ICustomerRepository customerRepository, ILoanTypeRepository loanTypeRepository, ILoanRepository loanRepository, ILoanDetailRepository loanDetailRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _loanTypeRepository = loanTypeRepository;
            _loanRepository = loanRepository;
            _loanDetailRepository = loanDetailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerList()
        {
            var customerList = _customerRepository.GetAll();
            return View(customerList);
        }
        public IActionResult AddCustomer()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (customer.CustomerId > 0)
            {
                return Json(_customerRepository.UpdateCustomer(customer));
            }
            else
            {
                return Json(_customerRepository.AddCustomer(customer));
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var customer = _customerRepository.GetById(id);
            return View("AddCustomer", customer);
        }

        public IActionResult DeleteCustomer(int id)
        {
            var deleteCustomer = _customerRepository.DeleteCustomer(id);
            return Json(deleteCustomer);
        }

        public IActionResult LoanTypeList()
        {
            var loanTypeList = _loanTypeRepository.GetAll();
            return View(loanTypeList);
        }

        public IActionResult AddLoanType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveLoanType(LoanType loanType)
        {
            if (loanType.LoanTypeId > 0)
            {
                return Json(_loanTypeRepository.UpdateLoanType(loanType));
            }
            else
            {
                return Json(_loanTypeRepository.AddLoanType(loanType));
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var loanType = _loanTypeRepository.GetById(id);
            return View("AddLoanType", loanType);
        }

        public IActionResult DeleteLoanType(int id)
        {
            var deleteLoanType = _loanTypeRepository.DeleteLoanType(id);
            return Json(deleteLoanType);
        }

        public IActionResult LoanList()
        {
            var loanList = _loanRepository.LoanList();
            return View(loanList);
        }

        public IActionResult AddLoan()
        {
            LoanDetailDTO details = new LoanDetailDTO();
            details.Customers = _customerRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.CustomerName + "(" + a.CustomerId + ")" ,
                Value = a.CustomerId.ToString()
            }).ToList();
            details.LoanTypes = _loanTypeRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.LoanName + "(" + a.LoanTypeId + ")",
                Value = a.LoanTypeId.ToString()
            }).ToList();
            return View(details);
        }

        //public IActionResult LoanDetail()
        //{
        //    DetailsDTO details = new DetailsDTO();
        //    details.Customers = _customerRepository.GetAll().Select(a => new SelectListItem
        //    {
        //        Text = a.CustomerName ,
        //        Value = a.CustomerId.ToString()
        //    }).ToList();
        //    details.LoanTypes = _loanTypeRepository.GetAll().Select(a => new SelectListItem
        //    {
        //        Text = a.LoanName,
        //        Value = a.LoanTypeId.ToString()
        //    }).ToList();
        //    return View(details);
        //}

        [HttpPost]
        public IActionResult SaveLoan(LoanDetailDTO loan)
        {
            if (loan.LoanId > 0)
            {
                return Json(_loanRepository.UpdateLoan(loan));
            }
            else
            {
                return Json(_loanRepository.AddLoan(loan));
            }
        }

        [HttpGet]
        public IActionResult EditLoan(int id)
        {
            LoanDetailDTO details = _loanRepository.GetLoanById(id);
            details.Customers = _customerRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.CustomerName,
                Value = a.CustomerId.ToString()
            }).ToList();
            details.LoanTypes = _loanTypeRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.LoanName,
                Value = a.LoanTypeId.ToString()
            }).ToList();
            //details.DateOfSanction = 
            return View("AddLoan", details);
        }



        public IActionResult LoanDetailList()
        {
            var loanDetailList = _loanDetailRepository.GetAll();
            return View(loanDetailList);
        }

        public IActionResult PayLoan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveLoanDetail(LoanDetails loanDetails)
        {
            if (loanDetails.LoanDetailsId > 0)
            {
                return Json(_loanDetailRepository.UpdatePayedLoan(loanDetails));
            }
            else
            {
                return Json(_loanDetailRepository.PayLoan(loanDetails));
            }
        }

        [HttpGet]
        public IActionResult EditPayedLoanDetail(int id)
        {
            var loanDetails = _loanDetailRepository.GetById(id);
            return View("PayLoan", loanDetails);
        }

        public IActionResult CloseLoan(int id)
        {
            var closeLoanDetail = _loanDetailRepository.LoanClosed(id);
            return Json(closeLoanDetail);
        }

        public IActionResult GetLoadDetailsById(int id)
        {
            var loanDetail = _loanDetailRepository.GetLoanandCustomerDetails(id);
            return Json(loanDetail);
        }

    }
}