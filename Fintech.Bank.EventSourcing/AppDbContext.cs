using Fintech.Bank.EventSourcing.Features.CreateBankAccount;
using Fintech.Bank.EventSourcing.Features.CreateTransaction;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Transaction>(transaction =>
        {
            transaction.HasKey(x => x.Id);

            transaction.HasOne(x => x.From)
                .WithMany()
                .IsRequired();

            transaction.HasOne(x => x.To)
                .WithMany()
                .IsRequired();
        });
    }
}