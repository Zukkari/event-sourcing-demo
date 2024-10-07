namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount;

public class AccountDto
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
}