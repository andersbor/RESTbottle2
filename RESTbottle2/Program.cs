using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTbottle2;
using RESTbottle2.Models;
using System.Text;

const bool useDatabase = false; // Set to true to use the database, false to use the in-memory list

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddAuthorization();


// Add services to the container.
const string AllowAllCORSpolicy = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllCORSpolicy,
       policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
       );
    options.AddPolicy(name: "zealandCORS",
        policy =>
        {
            policy.WithOrigins("https://www.zealand.dk/").WithMethods("GET").AllowAnyHeader();
        }
        );

});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.UseCors("zealandCORS");

app.UseAuthentication(); // Checks "Who are you?"
app.UseAuthorization();

app.MapControllers();

app.Run();
