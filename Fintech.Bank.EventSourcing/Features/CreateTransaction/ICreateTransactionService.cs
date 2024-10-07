namespace Fintech.Bank.EventSourcing.Features.CreateTransaction;

public interface ICreateTransactionService
{
    public Task<TransactionDto> CreateTransaction(CreateTransactionDto request);
}