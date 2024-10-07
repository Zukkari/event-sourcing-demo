using System.Text.Json;
using EventStore.Client;
using Fintech.Bank.EventSourcing.Domain;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;

public class CreateTransactionRepository(
    EventStoreProjectionManagementClient projectionManagementClient,
    EventStoreClient client) : ICreateTransactionRepository
{
    public async Task<Account> GetAccountById(Guid id)
    {
        var account = new Account();

        var balances =
            await projectionManagementClient.GetResultAsync<Dictionary<Guid, decimal>>("account_balance_projection_3");

        if (balances.TryGetValue(id, out var balance))
        {
            account.Id = id;
            account.Balance = balance;
        }

        return account;
    }

    public async Task<Guid> CreateTransaction(Account from, Account to, decimal amount)
    {
        var transactionId = Guid.NewGuid();

        var debitEvent = new DebitTransactionEvent
        {
            TransactionId = transactionId,
            Amount = amount,
            AccountId = from.Id
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
            Amount = amount,
            AccountId = to.Id
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