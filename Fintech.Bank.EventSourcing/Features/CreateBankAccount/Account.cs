namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount;

public class Account
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}