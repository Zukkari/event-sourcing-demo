namespace Fintech.Bank.EventSourcing.Domain;

public class TransactionEvent
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string? AccountNumber { get; set; } = string.Empty;
    public TransactionEventType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}