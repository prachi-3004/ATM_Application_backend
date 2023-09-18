using ATM.Models;

namespace ATM.Services
{
    public interface ICustomerService
    {
        public Customer GetCustomer(int id);

        public Customer GetCustomerDetail(Login login);
    }
}
