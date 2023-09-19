using ATM.Models;

namespace ATM.Services
{
    public interface ICustomerService
    {
        public Task<int> AddCustomer(Customer customer);

        public Task<Customer> GetCustomerByID(int id);

        public Task<Customer> GetCustomerDetail(Login login);

        public Task<List<Customer>> GetAllCustomers();

        public Task<int> DeleteCustomer(int id);

        public Task<int> UpdateCustomer(Customer customer);

        public Task<int> UpdateCredentials(int id, Login login);

    }
}
