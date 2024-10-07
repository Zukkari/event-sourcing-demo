using Fintech.Bank.EventSourcing.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;

public class CreateTransactionRepository(AppDbContext dbContext) : ICreateTransactionRepository
{
    public async Task<Account> GetAccountById(Guid id)
    {
        var account = new Account();

        await foreach (var transactionEvent in dbContext.TransactionEvents
                           .AsNoTracking()
                           .Where(x => x.AccountId == id)
                           .OrderBy(x => x.CreatedAt).AsAsyncEnumerable())
        {
            account.Apply(transactionEvent);
        }

        return account;
    }

    public async Task<Guid> CreateTransaction(Account from, Account to, decimal amount)
    {
        var transactionId = Guid.NewGuid();

        dbContext.TransactionEvents.Add(new TransactionEvent
        {
            Id = Guid.NewGuid(),
            AccountId = from.Id,
            Type = TransactionEventType.Debit,
            Amount = amount,
            CreatedAt = DateTime.UtcNow,
            TransactionId = transactionId
        });

        dbContext.TransactionEvents.Add(new TransactionEvent
        {
            Id = Guid.NewGuid(),
            AccountId = to.Id,
            Type = TransactionEventType.Credit,
            Amount = amount,
            CreatedAt = DateTime.UtcNow,
            TransactionId = transactionId
        });

        await dbContext.SaveChangesAsync();

        return transactionId;
    }
}