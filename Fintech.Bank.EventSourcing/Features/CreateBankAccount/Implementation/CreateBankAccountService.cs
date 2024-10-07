using Fintech.Bank.EventSourcing.Domain;

namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount.Implementation;

public class CreateBankAccountService(AppDbContext dbContext) : ICreateBankAccountService
{
    public async Task<AccountDto> CreateAccount(string accountNumber)
    {
        throw new NotImplementedException();
    }
}