using ATM.Models;

namespace ATM.Services
{
    public interface ICustomerService
    {
        public Task<Customer> GetCustomer(int id);

        public Task<Customer> GetCustomerDetail(Login login);
    }
}
