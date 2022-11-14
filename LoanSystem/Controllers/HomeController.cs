using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Castle.Core.Resource;

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
                    new Claim(ClaimTypes.Name, user.MobileNumber),
                    new Claim(ClaimTypes.Role, user.RoleName),
                    new Claim(ClaimTypes.Email, user.EmailId)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            
                return Redirect(login.ReturnUrl == null ? "/Home/Index" : login.ReturnUrl);
                
            }
            else
            {
                ViewBag.Message = "Invalid Credential";
                return View(login);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login","Home");
        }


        [Authorize(Roles = "Admin")]   
        public IActionResult CustomerList()
        {
            var customerList = _customerRepository.CustomerList();
            return View(customerList);
        }

        [Authorize]
        public IActionResult Detail(int id)
        {
            Customer customerDetail = _customerRepository.GetById(id);
            Role role = _customerRepository.GetRoleById(customerDetail.RoleId);
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.CustomerId = customerDetail.CustomerId;
            customerDTO.CustomerName = customerDetail.CustomerName;
            customerDTO.MobileNumber = customerDetail.MobileNumber;
            customerDTO.AadharNumber = customerDetail.AadharNumber;
            customerDTO.EmailId = customerDetail.EmailId;
            customerDTO.Password = customerDetail.Password;
            customerDTO.RoleName = role.RoleName;
            return View(customerDTO);
        }

        [Authorize]
        public IActionResult Information(int id)
        {
            var loan = _loanRepository.GetCustomer(id);
            return View(loan);
        }

        [Authorize]
        public IActionResult LoanInformation(int id)
        {
            LoanDetailList loanDetailList = new LoanDetailList();
            var customer = _customerRepository.GetById(id);
            loanDetailList.CustomerName = customer.CustomerName;
            var loan = _loanRepository.GetLoans().FirstOrDefault(x => x.CustomerId == id);
            loanDetailList.LoanId = loan.LoanId;
            loanDetailList.Customers = _loanDetailRepository.LoanDetailsById(id);
            return View(loanDetailList);
        }

        [Authorize(Roles = "Admin,Customer")]
        public IActionResult AddCustomer()
        {
            CustomerDTO customerDTO = new CustomerDTO();
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Identity.GetClaimRole();
                if (role == "Admin")
                {
                    customerDTO.Roles = _customerRepository.GetAllRole().Select(a => new SelectListItem
                    {
                        Text = a.RoleName + "(" + a.RoleId + ")",
                        Value = a.RoleId.ToString()
                    }).ToList();
                    customerDTO.Roles.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
                }
            }
            else
            {
                customerDTO.Roles = _customerRepository.GetAllRole().Where(x => x.RoleName == "Customer").Select(a => new SelectListItem
                {
                    Text = a.RoleName + "(" + a.RoleId + ")",
                    Value = a.RoleId.ToString()
                }).ToList();
            }
            return View(customerDTO);
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
            Customer customer = _customerRepository.GetById(id);
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.CustomerName = customer.CustomerName;
            customerDTO.MobileNumber = customer.MobileNumber;
            customerDTO.AadharNumber = customer.AadharNumber;
            customerDTO.EmailId = customer.EmailId;
            customerDTO.Password = customer.Password;
            customerDTO.RoleId = customer.RoleId;
            customerDTO.CustomerId = customer.CustomerId;
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Identity.GetClaimRole();
                if (role == "Admin")
                {
                    customerDTO.Roles = _customerRepository.GetAllRole().Select(a => new SelectListItem
                    {
                        Text = a.RoleName,
                        Value = a.RoleId.ToString()
                    }).ToList();
                    customerDTO.Roles.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
                }
                else
                {
                    customerDTO.Roles = _customerRepository.GetAllRole().Where(x => x.RoleName == "Customer").Select(a => new SelectListItem
                    {
                        Text = a.RoleName,
                        Value = a.RoleId.ToString()
                    }).ToList();
                }
            }         
            return View("AddCustomer", customerDTO);

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

        [Authorize]
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

        [Authorize(Roles ="Admin,Customer")]      
        
        public IActionResult AddLoan()
        {
            var id = User.Identity.GetClaimId();
            var role = User.Identity.GetClaimRole();
            LoanDetailDTO details = new LoanDetailDTO();
            if (role == "Admin")
            {
                details.Customers = _customerRepository.GetAll().Select(a => new SelectListItem
                {
                    Text = a.CustomerName + "(" + a.CustomerId + ")",
                    Value = a.CustomerId.ToString()
                }).ToList();
                details.Customers.Insert(0, new SelectListItem { Text = "Select Customer", Value = "" });
                
            }
            else
            {
                details.Customers = _customerRepository.GetAll().Where(x => x.CustomerId == x.CustomerId).Select(a => new SelectListItem
                {
                    Text = a.CustomerName + "(" + a.CustomerId + ")",
                    Value = a.CustomerId.ToString()
                }).ToList();
            }            
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
            Loan loan = _loanRepository.GetById(id);
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
            details.DateOfSanction = loan.DateOfSanction;
            return View("AddLoan", details);
        }



        [Authorize(Roles = "Admin,Customer")]
        public IActionResult LoanDetailList()
        {
            var loanDetailList = _loanDetailRepository.GetAll();
            return View(loanDetailList);
        }
        

        [Authorize]
        public IActionResult PayLoan(int id)
        {
            LoanDetailDTO loanDetailDTO = new LoanDetailDTO();
            var loan = _loanRepository.GetLoans().FirstOrDefault(x => x.CustomerId == id);
            //loanDetailDTO.LoanId = loan.LoanId;
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

        public IActionResult GetLoanDetailsId(int id)
        {
            var loanDetail = _loanDetailRepository.GetLoanandCustomerDetails(id);
            return Json(loanDetail);
        }


       
    }
}