using Euronext.Domain.Entities;

namespace Euronext.Domain.Repository
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetWeekAsync(DateOnly date);
        Task<WeatherForecast> GetByDateAsync(DateOnly date);
        Task<WeatherForecast> CreateAsync(WeatherForecast weatherForecast);
        Task<int> UpdateAsync(DateOnly date, WeatherForecast weatherForecast);
        Task<int> DeleteAsync(DateOnly date);
    }
}
