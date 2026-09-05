using Microsoft.EntityFrameworkCore;
using JobTrackin.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

// Oracle + EF Core
var oracleConnectionString =
    builder.Configuration.GetConnectionString("Oracle")
    ?? builder.Configuration["ORACLE_CONNECTION_STRING"];

if (string.IsNullOrWhiteSpace(oracleConnectionString))
{
    throw new InvalidOperationException(
        "Oracle connection string is not configured.");
}

builder.Services.AddDbContext<JobTrackinDbContext>(options =>
{
    options.UseOracle(oracleConnectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();