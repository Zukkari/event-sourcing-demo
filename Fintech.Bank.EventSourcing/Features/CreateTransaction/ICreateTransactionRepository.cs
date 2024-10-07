namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public interface ICreateTransactionRepository
{
    Task<Account> GetAccountById(Guid id);

    Task<Guid> CreateTransaction(Account from, Account to, decimal amount);
}