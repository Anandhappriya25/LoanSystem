using Castle.Core.Resource;
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
            _customer.Password = customers.Password;
            _customer.RoleId = customers.RoleId;
            Messages message = new Messages();
            message.Role = customers.Role;
            message.Success = false;
            //var customer = _loanDbContext.Customer.Where(x => x.MobileNumber == customers.MobileNumber || x.AadharNumber == customers.AadharNumber).SingleOrDefault();
            var mobile = GetByMobileNumber(_customer.MobileNumber);
            var aadhar = GetByAadharNumber(_customer.AadharNumber);
            var email = GetByEmailId(_customer.EmailId);
            if (mobile != null)
            {
                message.Message = "Mobile Number is already exists";
                return message;
            }
            if (aadhar != null)
            {
                message.Message = "Aadhar Number is already exists";
                return message;
            }
            if (email != null)
            {
                message.Message = "MEmailId already Registered.";
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
            var customer = _loanDbContext.Customer.FirstOrDefault(x => x.CustomerId == id);
            return customer;
        }

        public Customer GetByName(string name)
        {
            return _loanDbContext.Customer.FirstOrDefault(x => x.CustomerName == name);
        }
        public Customer GetByMobileNumber(string mobileNumber)
        {
            var mobile = _loanDbContext.Customer.FirstOrDefault(x => x.MobileNumber == mobileNumber);
            return mobile;
        }
        public Customer GetByAadharNumber(long aadharNumber)
        {
            var aadhar = _loanDbContext.Customer.FirstOrDefault(x => x.AadharNumber == aadharNumber);
            return aadhar;
        }
        public Customer GetByEmailId(string emailId)
        {
            var email = _loanDbContext.Customer.FirstOrDefault(x => x.EmailId == emailId);
            return email;
        }

        public Messages UpdateCustomer(CustomerDTO customer)
        {
            Customer customers = new Customer();
            customers.CustomerId = customer.CustomerId;
            customers.CustomerName = customer.CustomerName;
            customers.MobileNumber = customer.MobileNumber;
            customers.AadharNumber = customer.AadharNumber;
            customers.EmailId = customer.EmailId;
            customers.Password = customer.Password;
            customers.RoleId = customer.RoleId;
            Messages message = new Messages();
            message.Success = false;
            var customerId = GetById(customers.CustomerId);
            var mobileNumber = GetByMobileNumber(customers.MobileNumber);
            var aadharNumber = GetByAadharNumber(customers.AadharNumber);
            if (customerId == null)
            {
                message.Message = "Customer Id not found";
                return message;
            }     
            if(mobileNumber != null && mobileNumber.CustomerId != customerId.CustomerId )
            {
                message.Message = "Can't modify ";
                return message;
            }
            var loan = _loanDbContext.Loan.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            if (loan == null)
            {
                customerId.CustomerName = customer.CustomerName;
                customerId.MobileNumber = customer.MobileNumber;
                customerId.AadharNumber = customer.AadharNumber;
                customerId.EmailId = customer.EmailId;
                customerId.Password = customer.Password; 
                _loanDbContext.SaveChanges();
                message.Success = true;
                message.Message = "Customer Details modified";
                return message;
            }
            else
            {
                message.Message = "Can't modify Customer because they already applied loan";
            }
            var customerDto = _loanDbContext.Customer.FirstOrDefault(x => x.CustomerId == customers.CustomerId);
            return message;
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


        
        public CustomerDTO GetLoginDetail(string emailId, string password)
        {
            var customers = (from customer in _loanDbContext.Customer
                             join role in _loanDbContext.Roles on customer.RoleId equals role.RoleId
                             where customer.EmailId == emailId && customer.Password == password
                             select new CustomerDTO()
                             {
                                 CustomerId = customer.CustomerId,
                                 MobileNumber = customer.MobileNumber,
                                 RoleName = role.RoleName,
                                 EmailId = customer.EmailId
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