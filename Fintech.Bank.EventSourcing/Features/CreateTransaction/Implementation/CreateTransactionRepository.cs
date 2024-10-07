using System.Text.Json;
using EventStore.Client;
using Fintech.Bank.EventSourcing.Domain;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;

public class CreateTransactionRepository(EventStoreClient client) : ICreateTransactionRepository
{
    public async Task<Account> GetAccountById(Guid id)
    {
        var account = new Account();

        await foreach (var accountEvent in client
                           .ReadStreamAsync(Direction.Forwards, $"account-{id}", StreamPosition.Start)
                           .AsAsyncEnumerable())
        {
            switch (accountEvent.Event.EventType)
            {
                case nameof(TransactionEventType.Initialized):
                    var initializedEvent =
                        JsonSerializer.Deserialize<InitializeAccountEvent>(accountEvent.Event.Data.Span);
                    ArgumentNullException.ThrowIfNull(initializedEvent);

                    initializedEvent.Apply(account);
                    break;
                case nameof(TransactionEventType.Debit):
                    var debitEvent = JsonSerializer.Deserialize<DebitTransactionEvent>(accountEvent.Event.Data.Span);
                    ArgumentNullException.ThrowIfNull(debitEvent);

                    debitEvent.Apply(account);
                    break;
                case nameof(TransactionEventType.Credit):
                    var creditEvent = JsonSerializer.Deserialize<CreditTransactionEvent>(accountEvent.Event.Data.Span);
                    ArgumentNullException.ThrowIfNull(creditEvent);

                    creditEvent.Apply(account);
                    break;
            }
        }

        return account;
    }

    public async Task<Guid> CreateTransaction(Account from, Account to, decimal amount)
    {
        var transactionId = Guid.NewGuid();

        var debitEvent = new DebitTransactionEvent
        {
            TransactionId = transactionId,
            Amount = amount
        };

        var debitEventData = new EventData(
            Uuid.NewUuid(),
            nameof(TransactionEventType.Debit),
            JsonSerializer.SerializeToUtf8Bytes(debitEvent));

        await client.AppendToStreamAsync(
            $"account-{from.Id}",
            StreamState.Any,
            [debitEventData]);

        var creditEvent = new CreditTransactionEvent
        {
            TransactionId = transactionId,
            Amount = amount
        };

        var creditEventData = new EventData(
            Uuid.NewUuid(),
            nameof(TransactionEventType.Credit),
            JsonSerializer.SerializeToUtf8Bytes(creditEvent));

        await client.AppendToStreamAsync(
            $"account-{to.Id}",
            StreamState.Any,
            [creditEventData]);

        return transactionId;
    }
}