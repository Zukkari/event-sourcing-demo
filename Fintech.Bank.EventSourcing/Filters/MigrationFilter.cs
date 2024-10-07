using Microsoft.EntityFrameworkCore;

namespace Fintech.Bank.EventSourcing.Filters;

public class MigrationFilter(IConfiguration configuration) : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            var connectionString = configuration.GetConnectionString("ServiceConnection");
            
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString).Options;
            using var dbContext = new AppDbContext(dbOptions);
            
            dbContext.Database.Migrate();

            next(builder);
        };
    }
}