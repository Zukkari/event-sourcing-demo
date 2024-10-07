using Fintech.Bank.EventSourcing.Features.CreateTransaction;

namespace Fintech.Bank.EventSourcing.Domain;

public class InitializeAccountEvent : IAccountEvent
{
    public Guid AccountId { get; set; }
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public void Apply(Account account)
    {
        account.Id = AccountId;
        account.AccountNumber = AccountNumber;
        account.Balance = Balance;
    }
}