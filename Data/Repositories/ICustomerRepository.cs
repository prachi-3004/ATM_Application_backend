using ATM.Models;

namespace ATM.Data.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Customer> GetCustomer(int id);

        public Task<Customer> GetCustomerDetail(Login login);
    }
}
