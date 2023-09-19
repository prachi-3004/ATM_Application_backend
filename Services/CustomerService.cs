using ATM.Data;
using ATM.Models;
using ATM.Data.Repositories;

namespace ATM.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _custRepository;
        private readonly IAccountService _accountService;
        public CustomerService(ICustomerRepository custRepository, IAccountService accountService)
        {
            _custRepository = custRepository;
            _accountService = accountService;
        }

        public async Task<int> AddCustomer(Customer cust)
        {
            return await _custRepository.AddCustomer(cust);
        }

        public async Task<Customer> GetCustomerByID(int id)
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
            var customer = await _custRepository.GetCustomerByID(id);
            foreach (Account account in customer.Accounts)
            {
                await _accountService.CloseAccount(id);
            }
            return await _custRepository.DeleteCustomer(id);
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            return await _custRepository.UpdateCustomer(customer);
        }

        public async Task<int> UpdateCredentials(int id, Login login)
        {
            return await _custRepository.UpdateCredentials(id, login);
        }
    }
}
