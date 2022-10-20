using LoanSystem.Models;

namespace LoanSystem.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        Customer GetByName(string name);
        Customer GetByMobileNumber(int mobileNumber); 
        Messages AddCustomer(Customer customer);
        Messages UpdateCustomer(Customer customer);
        Messages DeleteCustomer(int id);
    }
}