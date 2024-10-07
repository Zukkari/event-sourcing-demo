using Fintech.Bank.EventSourcing.Features.CreateBankAccount;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasKey(x => x.Id);
    }
}