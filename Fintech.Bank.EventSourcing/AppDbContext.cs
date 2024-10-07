using Fintech.Bank.EventSourcing.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TransactionEvent> TransactionEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionEvent>(transactionEvent =>
        {
            transactionEvent.HasKey(x => x.Id);

            transactionEvent.HasIndex(x => x.AccountId);
            transactionEvent.HasIndex(x => x.CreatedAt);
        });
    }
}