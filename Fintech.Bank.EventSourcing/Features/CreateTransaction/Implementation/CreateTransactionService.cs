namespace Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;

public class CreateTransactionService(
    ICreateTransactionRepository repository) : ICreateTransactionService
{
    public async Task<TransactionDto> CreateTransaction(CreateTransactionDto request)
    {
        var accountFrom = await repository.GetAccountById(request.From);
        var accountTo = await repository.GetAccountById(request.To);

        if (accountFrom.Balance < request.Amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        var transactionId = await repository.CreateTransaction(accountFrom, accountTo, request.Amount);

        return new TransactionDto
        {
            Id = transactionId,
            Amount = request.Amount
        };
    }
}