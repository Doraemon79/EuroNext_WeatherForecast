using Euronext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroNext.Application.Services
{
    public interface IEuronextService
    {
        Task<List<WeatherForecastDisplay>> GetWeekAsync(DateOnly date);
        Task<WeatherForecastDisplay> GetByDateAsync(DateOnly date);
        Task<WeatherForecast> CreateAsync(WeatherForecast weatherForecast);
        Task<int> UpdateAsync(DateOnly date, WeatherForecast weatherForecast);
        Task<int> DeleteAsync(DateOnly date);
    }
}
