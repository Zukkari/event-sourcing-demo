using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;

public class CreateTransactionService(AppDbContext dbContext) : ICreateTransactionService
{
    public async Task<TransactionDto> CreateTransaction(CreateTransactionDto request)
    {
        var accountFrom = await dbContext.Accounts.FirstAsync(x => x.Id == request.From);
        var accountTo = await dbContext.Accounts.FirstAsync(x => x.Id == request.To);

        if (accountFrom.Balance < request.Amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            From = accountFrom,
            To = accountTo,
            Amount = request.Amount,
            CreatedAt = DateTime.UtcNow
        };

        transaction.Complete();

        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync();

        return new TransactionDto
        {
            Id = transaction.Id,
            Amount = transaction.Amount
        };
    }
}