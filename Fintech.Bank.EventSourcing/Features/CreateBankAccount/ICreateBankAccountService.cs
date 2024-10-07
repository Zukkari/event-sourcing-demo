namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount;

public interface ICreateBankAccountService
{
    Task<AccountDto> CreateAccount(string accountNumber);
}