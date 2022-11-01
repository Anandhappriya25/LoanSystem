using LoanSystem.Models;
using System.Data;

namespace LoanSystem.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        Customer GetByName(string name);
        Customer GetByMobileNumber(int mobileNumber); 
        Messages AddCustomer(CustomerDTO customer);
        Messages UpdateCustomer(CustomerDTO customer);
        Messages DeleteCustomer(int id);
        CustomerDTO GetLoginDetail(string emailId, string password);
        IEnumerable<CustomerDTO> CustomerList();
        List<Role> GetAllRole();
        Role GetRoleById(int id);        
    }
}