namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount.Implementation;

public class CreateBankAccountService(AppDbContext dbContext) : ICreateBankAccountService
{
    public async Task<AccountDto> CreateAccount(string accountNumber)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountNumber,
            Balance = 0,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();

        return new AccountDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber
        };
    }
}