using System.Text.Json;
using EventStore.Client;
using Fintech.Bank.EventSourcing.Domain;

namespace Fintech.Bank.EventSourcing.Features.CreateBankAccount.Implementation;

public class CreateBankAccountService(EventStoreClient client) : ICreateBankAccountService
{
    public async Task<AccountDto> CreateAccount(string accountNumber)
    {
        var accountId = Guid.NewGuid();

        var transactionEvent = new InitializeAccountEvent
        {
            AccountId = accountId,
            AccountNumber = accountNumber,
            Balance = 1_000_000
        };

        var eventData = new EventData(
            Uuid.NewUuid(),
            nameof(TransactionEventType.Initialized),
            JsonSerializer.SerializeToUtf8Bytes(transactionEvent));

        await client.AppendToStreamAsync(
            $"account-{accountId}",
            StreamState.NoStream,
            [eventData]);

        return new AccountDto
        {
            Id = accountId,
            AccountNumber = accountNumber
        };
    }
}