namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public class Account
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}