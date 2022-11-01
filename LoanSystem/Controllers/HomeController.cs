using LoanSystem.Models;
using LoanSystem.Models.DTO;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = _customerRepository.GetLoginDetail(login.EmailId, login.Password);
            if (user != null)
            {
                var claims = new List<Claim>

                {
                    new Claim(ClaimTypes.NameIdentifier , user.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, user.EmailId),
                    new Claim(ClaimTypes.Role, user.RoleName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                if (user.RoleName == "Admin")
                {
                    return Redirect(login.ReturnUrl == null ? "/Home/Index" : login.ReturnUrl);
                }
                else
                {
                    return Redirect(login.ReturnUrl == null ? "/Home/AddCustomer" : login.ReturnUrl);
                }
            }
            else
            {
                ViewBag.Message = "Invalid Credential";
                return View(login);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Index");
        }
        [Authorize]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult CustomerList()
        {
            var customerList = _customerRepository.CustomerList();
            return View(customerList);
        }
        
        public IActionResult AddCustomer()
        {
            CustomerDTO role = new CustomerDTO();
            role.Roles = _customerRepository.GetAllRole().Select(a => new SelectListItem
            {
                Text = a.RoleName,
                Value = a.RoleId.ToString()
            }).ToList();
            role.Roles.Insert(0, new SelectListItem { Text = "Select Role", Value = ""});
            return View(role);
        }

        [HttpPost]
        public IActionResult Save(CustomerDTO customer)
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
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Update(int id)
        {
            var customer = _customerRepository.GetById(id);
            return View("AddCustomer", customer);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCustomer(int id)
        {
            var deleteCustomer = _customerRepository.DeleteCustomer(id);
            return Json(deleteCustomer);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var loanType = _loanTypeRepository.GetById(id);
            return View("AddLoanType", loanType);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLoanType(int id)
        {
            var deleteLoanType = _loanTypeRepository.DeleteLoanType(id);
            return Json(deleteLoanType);
        }

        [Authorize(Roles = "Admin")]
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
            details.Customers.Insert(0, new SelectListItem { Text = "Select Customer", Value = "" });
            details.LoanTypes = _loanTypeRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.LoanName + "(" + a.LoanTypeId + ")",
                Value = a.LoanTypeId.ToString()
            }).ToList();
            details.LoanTypes.Insert(0, new SelectListItem { Text = "Select LoanType", Value = "" });
            return View(details);
        }

        

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
        [Authorize(Roles = "Admin")]
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



        [Authorize(Roles = "Admin,Customer")]
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

        [Authorize(Roles = "Admin,Customer")]
        public IActionResult EditPayedLoanDetail(int id)
        {
            var loanDetails = _loanDetailRepository.GetById(id);
            return View("PayLoan", loanDetails);
        }

        [Authorize(Roles = "Admin,Customer")]
        public IActionResult CloseLoan(int id)
        {
            var closeLoanDetail = _loanDetailRepository.LoanClosed(id);
            return Json(closeLoanDetail);
        }

        public IActionResult GetLoanDetailsById(int id)
        {
            var loanDetail = _loanDetailRepository.GetLoanandCustomerDetails(id);
            return Json(loanDetail);
        }

    }
}