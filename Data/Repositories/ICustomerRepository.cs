using ATM.Models;

namespace ATM.Data.Repositories
{
    public interface ICustomerRepository
    {
        public Customer GetCustomer(int id);

        public Customer GetCustomerDetail(Login login);
    }
}
