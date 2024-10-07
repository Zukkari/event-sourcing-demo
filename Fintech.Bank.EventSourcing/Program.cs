using EventStore.Client;
using Fintech.Bank.EventSourcing.Features.CreateBankAccount;
using Fintech.Bank.EventSourcing.Features.CreateBankAccount.Implementation;
using Fintech.Bank.EventSourcing.Features.CreateTransaction;
using Fintech.Bank.EventSourcing.Features.CreateTransaction.Implementation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateBankAccountService, CreateBankAccountService>();
builder.Services.AddScoped<ICreateTransactionService, CreateTransactionService>();
builder.Services.AddScoped<ICreateTransactionRepository, CreateTransactionRepository>();

builder.Services.AddScoped<EventStoreClient>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("ServiceConnection");
    ArgumentNullException.ThrowIfNull(connectionString);

    var settings = EventStoreClientSettings.Create(connectionString);

    return new EventStoreClient(settings);
});

var app = builder.Build();

app.MapPost("/api/v1/accounts",
    async ([FromBody] string accountNumber, [FromServices] ICreateBankAccountService service) =>
    {
        var account = await service.CreateAccount(accountNumber);
        return Results.Ok(account);
    });

app.MapPost("/api/v1/transactions",
    async ([FromBody] CreateTransactionDto createTransactionDto, [FromServices] ICreateTransactionService service) =>
    {
        var transaction = await service.CreateTransaction(createTransactionDto);
        return Results.Ok(transaction);
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.RunAsync();