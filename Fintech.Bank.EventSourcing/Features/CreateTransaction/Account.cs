using Fintech.Bank.EventSourcing.Domain;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public class Account
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }

    public void Apply(TransactionEvent transactionEvent)
    {
        switch (transactionEvent.Type)
        {
            case TransactionEventType.Initialized:
                Balance = transactionEvent.Amount;
                AccountNumber = transactionEvent.AccountNumber ?? string.Empty;
                break;
            case TransactionEventType.Credit:
                Balance += transactionEvent.Amount;
                break;
            case TransactionEventType.Debit:
                Balance -= transactionEvent.Amount;
                break;
            default:
                throw new ArgumentOutOfRangeException($"Invalid event type: {transactionEvent.Type}");
        }
    }
}