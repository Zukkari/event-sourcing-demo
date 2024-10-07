using Fintech.Bank.EventSourcing.Domain;

namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount.Implementation;

public class CreateBankAccountService(AppDbContext dbContext) : ICreateBankAccountService
{
    public async Task<AccountDto> CreateAccount(string accountNumber)
    {
        var transactionEvent = new TransactionEvent
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            AccountNumber = accountNumber,
            Type = TransactionEventType.Initialized,
            Amount = 1_000_000,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.TransactionEvents.Add(transactionEvent);
        await dbContext.SaveChangesAsync();

        return new AccountDto
        {
            Id = transactionEvent.Id,
            AccountNumber = accountNumber
        };
    }
}