using Fintech.Bank.EventSourcing.Features.CreateTransaction;

namespace Fintech.Bank.EventSourcing.Domain;

public interface IAccountEvent
{
    void Apply(Account account);
}