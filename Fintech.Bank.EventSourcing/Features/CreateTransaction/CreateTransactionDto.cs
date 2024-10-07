namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public class CreateTransactionDto
{
    public Guid From { get; set; }
    public Guid To { get; set; }
    public decimal Amount { get; set; }
}