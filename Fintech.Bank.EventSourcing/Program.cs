using Fintech.Bank.EventSourcing;
using Fintech.Bank.EventSourcing.Features.CreateBankAccount;
using Fintech.Bank.EventSourcing.Features.CreateBankAccount.Implementation;
using Fintech.Bank.EventSourcing.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateBankAccountService, CreateBankAccountService>();

builder.Services.AddTransient<IStartupFilter, MigrationFilter>();

builder.Services.AddDbContext<AppDbContext>((_, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("ServiceConnection");
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
});

var app = builder.Build();

app.MapPost("/api/v1/accounts",
    async ([FromBody] string accountNumber, [FromServices] ICreateBankAccountService service) =>
    {
        var account = await service.CreateAccount(accountNumber);
        return Results.Ok(account);
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.RunAsync();