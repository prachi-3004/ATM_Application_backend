using ATM.Models;

namespace ATM.Data.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Customer> GetCustomerByID(int id);

        public Task<Customer> GetCustomerDetail(Login login);

        public Task<List<Customer>> GetAllCustomers();

        public Task<int> UpdateCustomer(Customer customer);

        public Task<int> UpdateCredentials(int id, Login login);

        public Task<int> DeleteCustomer(int id);
    }
}
