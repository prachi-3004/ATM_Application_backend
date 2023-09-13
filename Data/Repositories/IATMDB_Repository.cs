using ATM_banking_system.Models;

namespace ATM_banking_system.Data.Repositories
{
    public interface IATMDB_Repository
    {
        public Customer GetCustomer(int id);

        public Customer GetCustomerDetail(CustomerLogin login);
    }
}
