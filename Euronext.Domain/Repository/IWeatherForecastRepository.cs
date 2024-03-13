using Euronext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euronext.Domain.Repository
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetWeekAsync(DateOnly date);
        Task<WeatherForecast> GetByDateAsync(DateOnly date);
        Task<WeatherForecast> CreateAsync(WeatherForecast weatherForecast);
        Task<int> UpdateAsync(DateOnly date,WeatherForecast weatherForecast);
        Task<int> DeleteAsync(DateOnly date);
    }
}
