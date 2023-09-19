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
            return await _custRepository.GetCustomerByID(id);
        }

        public async Task<Customer> GetCustomerDetail(Login login)
        {
            return await _custRepository.GetCustomerDetail(login);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _custRepository.GetAllCustomers();
        }

        public async Task<int> DeleteCustomer(int id)
        {
            return await _custRepository.DeleteCustomer(id);
        }
    }
}
