using ATM_banking_system.Data;
using ATM_banking_system.Models;
using ATM_banking_system.Data.Repositories;

namespace ATM_banking_system.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IATMDB_Repository _provider;
        public CustomerService(IATMDB_Repository provider)
        {
            _provider = provider;
        }
        public Customer GetCustomer(int id)
        {
            return _provider.GetCustomer(id);
        }

        public Customer GetCustomerDetail(CustomerLogin login)
        {
            return _provider.GetCustomerDetail(login);
        }
    }
}
