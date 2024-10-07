using Fintech.Bank.EventSourcing.Features.CreateTransaction;

namespace Fintech.Bank.EventSourcing.Domain;

public class CreditTransactionEvent : IAccountEvent
{
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }

    public void Apply(Account account)
    {
        account.Balance += Amount;
    }
}