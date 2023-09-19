using ATM.Data.Repositories;
using ATM.Models;

namespace ATM.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        //public async Task<int> ProcessTransaction(TransactionRequest request)
        //{
        //    if (request.Type == "Transfer" && request.SenderId != null && request.RecipientId != null)
        //    {
        //        Transaction transaction = new Transaction();
        //        Transaction transaction1 = new Transaction();
        //        transaction.Type = "Debit";
        //        transaction1.Type = "Credit";
        //        transaction.AccountId = request.SenderId;
        //        transaction1.AccountId = request.RecipientId;
        //        transaction.Amount = request.Amount;
        //        transaction1.Amount = request.Amount;
        //        transaction.Time = DateTime.Now;
        //        transaction1.Time = DateTime.Now;
        //        var account = await _accountRepository.GetAccountByID((int)request.SenderId);
        //        var account1 = await _accountRepository.GetAccountByID((int)request.RecipientId);
        //        transaction.Account = account;
        //        transaction1.Account = account1;

        //    }
        //}
    }
}
