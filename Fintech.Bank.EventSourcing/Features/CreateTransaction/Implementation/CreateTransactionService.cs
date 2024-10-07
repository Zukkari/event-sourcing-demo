using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;

public class CreateTransactionService(AppDbContext dbContext) : ICreateTransactionService
{
    public async Task<TransactionDto> CreateTransaction(CreateTransactionDto request)
    {
        throw new NotImplementedException();
    }
}