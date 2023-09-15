using ATM_banking_system.Data;
using ATM_banking_system.Models;
using System.Linq;

namespace ATM_banking_system.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ATMContext _context;

        public TransactionService( ATMContext context )
        {
            _context = context;
        }

        public bool AddTransaction(Transaction transaction)
        {
            //int maxTrans = 0;
            //if (_context.Transactions.Any())
            //{
            //    maxTrans = _context.Transactions.Max(t => t.TransactionId)+1;
            //}
            return true;
            
        }
    }
}
