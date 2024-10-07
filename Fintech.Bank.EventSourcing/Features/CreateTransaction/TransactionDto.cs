namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
}