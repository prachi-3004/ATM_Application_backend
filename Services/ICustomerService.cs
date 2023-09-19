using ATM.Models;

namespace ATM.Services
{
    public interface ICustomerService
    {
        public Task<Customer> GetCustomer(int id);

        public Task<Customer> GetCustomerDetail(Login login);

        public Task<List<Customer>> GetAllCustomers();

        public Task<int> DeleteCustomer(int id);


    }
}
