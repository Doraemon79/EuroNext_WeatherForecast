using Euronext.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EuroNext.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WeatherForecast>().HasKey(w => w.Date);
            builder.Entity<WeatherForecast>().ToTable("WeatherForecasts");
        }
    }
}
