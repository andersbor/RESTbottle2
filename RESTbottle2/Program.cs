using Microsoft.EntityFrameworkCore;
using RESTbottle2;
using RESTbottle2.Models;

const bool useDatabase = false; // Set to true to use the database, false to use the in-memory list

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

if (useDatabase)
{
    var optionsBuilder = new DbContextOptionsBuilder<BottlesDbContext>();
    // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
    optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
    // connection string structure
    //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
    BottlesDbContext _dbContext = new(optionsBuilder.Options);

    builder.Services.AddSingleton<IBottlesRepository>(
        new BottlesRepositoryDatabaseEF(_dbContext));
}
else
{
    builder.Services.AddSingleton<IBottlesRepository>(
        new BottlesRepositoryList(includeTestData: true));
}
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
