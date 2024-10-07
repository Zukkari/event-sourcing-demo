using Fintech.Bank.EventSourcing.Features.CreateBankAccount;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public class Transaction
{
    public Guid Id { get; set; }
    public required Account From { get; set; }
    public required Account To { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public void Complete()
    {
        From.Balance -= Amount;
        To.Balance += Amount;
    }
}