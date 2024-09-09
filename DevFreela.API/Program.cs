using DevFreela.API.ExceptionHandlers;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Application;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FreelanceTotalCostConfig>(
    builder.Configuration.GetSection("FreelanceTotalCostConfig")
    );

//builder.Services.AddDbContext<DevFreelaDbContext>(o => o.UseInMemoryDatabase("DevFreelaDb"));
var connectionString = builder.Configuration.GetConnectionString("DevFreelaCS");
builder.Services.AddDbContext<DevFreelaDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddApplication();

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
