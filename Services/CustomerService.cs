using ATM.Data;
using ATM.Models;
using ATM.Data.Repositories;

namespace ATM.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _custRepository;
        public CustomerService(ICustomerRepository custRepository)
        {
            _custRepository = custRepository;
        }
        public Customer GetCustomer(int id)
        {
            return _custRepository.GetCustomer(id);
        }

        public Customer GetCustomerDetail(Login login)
        {
            return _custRepository.GetCustomerDetail(login);
        }
    }
}
