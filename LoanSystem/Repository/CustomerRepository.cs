using LoanSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace LoanSystem.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LoanDbContext _loanDbContext;
        public CustomerRepository(LoanDbContext loanDbContext)
        {
            _loanDbContext = loanDbContext;
        }

        public Messages AddCustomer(CustomerDTO customers)
        {
            Customer _customer = new Customer();
            _customer.CustomerId = customers.CustomerId;
            _customer.CustomerName = customers.CustomerName;
            _customer.MobileNumber = customers.MobileNumber;
            _customer.AadharNumber = customers.AadharNumber;
            _customer.EmailId = customers.EmailId;
            _customer.RoleId = customers.RoleId;
            Messages message = new Messages();
            var customer = _loanDbContext.Customer.Where(x => x.MobileNumber == customers.MobileNumber || x.AadharNumber == customers.AadharNumber).SingleOrDefault();
            if (customer != null)
            {
                message.Success = false;
                message.Message = "Mobile Number or Aadhar Number is already exists";
                return message;
            }
            else
            {
                _loanDbContext.Customer.Add(_customer);
                _loanDbContext.SaveChanges();
                message.Success = true;
                message.Message = "Customer added";
                return message;
            }
        }
        public List<Customer> GetAll()
        {
            return _loanDbContext.Customer.ToList();
        }

        public Customer GetById(int id)
        {
            return _loanDbContext.Customer.Find(id);
        }

        public Customer GetByName(string name)
        {
            return _loanDbContext.Customer.FirstOrDefault(x => x.CustomerName == name);
        }

        public Messages UpdateCustomer(CustomerDTO customer)
        {
            Messages message = new Messages();
            message.Success = false;
            var _customer = _loanDbContext.Customer.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            var loan = _loanDbContext.Loan.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            if (loan == null)
            {
                _customer.CustomerName = customer.CustomerName;
                _customer.MobileNumber = customer.MobileNumber;
                _customer.AadharNumber = customer.AadharNumber;
                _loanDbContext.SaveChanges();
                message.Message = "Customer Details modified";
                return message;
            }
            else
            {
                message.Message = "Can't modify Customer because they already applied loan";
                return message;
            }

        }
        public Messages DeleteCustomer(int id)
        {
            Messages message = new Messages();
            message.Success = false;
            var customer = _loanDbContext.Customer.Where(x => x.CustomerId == id).FirstOrDefault();
            var loan = _loanDbContext.Loan.Where(x => x.CustomerId == id).FirstOrDefault();
            if (loan != null)
            {
                message.Success = false;
                message.Message = "This Customer id can't be deleted because they applied the loan";
                return message;
            }
            else
            {
                _loanDbContext.Customer.Remove(customer);
                _loanDbContext.SaveChanges();
                message.Success = true;
                message.Message = "Customer removed";
                return message;
            }
        }

        public Customer GetByMobileNumber(int mobileNumber)
        {
            return _loanDbContext.Customer.FirstOrDefault(x => x.MobileNumber == x.MobileNumber);
        }

        public LoginResultDTO GetLoginDetail(string emailId, string password)
        {
            var customers = (from customer in _loanDbContext.Customer
                             join role in _loanDbContext.Roles on customer.RoleId equals role.RoleId
                             where /*customer.Password == password &&*/ customer.EmailId == emailId
                             select new LoginResultDTO()
                             {
                                 CustomerId = customer.CustomerId,
                                 MobileNumber = customer.MobileNumber,
                                 RoleName = role.RoleName,
                                 Email = customer.EmailId
                             }).FirstOrDefault();
            return customers;
        }

        public Role GetRoleById(int id)
        {
            return _loanDbContext.Roles.Find(id);
        }

        public List<Role> GetAllRole()
        {
            return _loanDbContext.Roles.ToList();
        }

        public IEnumerable<CustomerDTO> CustomerList()
        {
            var customer = (from customers in _loanDbContext.Customer
                            join cus in _loanDbContext.Customer on customers.RoleId equals cus.RoleId
                            join role in _loanDbContext.Roles on customers.RoleId equals role.RoleId
                            //where role.RoleId == customers.RoleId
                            select new CustomerDTO()
                            {
                                CustomerId = customers.CustomerId,
                                CustomerName = customers.CustomerName,
                                MobileNumber = customers.MobileNumber,
                                AadharNumber = customers.AadharNumber,
                                EmailId = customers.EmailId,
                                RoleName = role.RoleName
                            }).ToList();
            return customer;
        }
    }
}