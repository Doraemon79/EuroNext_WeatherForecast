using Euronext.Domain.Repository;
using EuroNext.Application.Services;
using EuroNext.Infrastructure;
using EuroNext.Infrastructure.Repositories;
using EuroNext.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));

builder.Services.AddScoped<RequestValidator>();
builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddTransient<IEuronextService, EuronextService>();
// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Services.AddControllers();

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

public partial class Program
{
}
