using CreditCardAPI.Data;
using CreditCardAPI.Repositories;
using CreditCardAPI.Services;
using CreditCardAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(opt =>
{
    opt.UseInMemoryDatabase(databaseName: "ApiDatabase");
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<CustomerRepository, CustomerRepository>();
builder.Services.AddTransient<CreditCardRepository, CreditCardRepository>();

builder.Services.AddTransient<ICreditCardService, CredtiCardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
