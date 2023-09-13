using ATM_banking_system.Models;

namespace ATM_banking_system.Services
{
    public interface ICustomerService
    {
        public Customer GetCustomer(int id);

        public Customer GetCustomerDetail(CustomerLogin login);
    }
}
