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
        public async Task<Customer> GetCustomer(int id)
        {
            return await _custRepository.GetCustomer(id);
        }

        public async Task<Customer> GetCustomerDetail(Login login)
        {
            return await _custRepository.GetCustomerDetail(login);
        }
    }
}
