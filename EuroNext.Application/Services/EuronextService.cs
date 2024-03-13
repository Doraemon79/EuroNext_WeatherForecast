using Euronext.Domain.Entities;
using Euronext.Domain.Repository;
using EuroNext.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroNext.Application.Services
{
    public class EuronextService : IEuronextService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        public EuronextService(IWeatherForecastRepository weatherForecastRepository)
        {

            _weatherForecastRepository = weatherForecastRepository;

        }

        public async Task<WeatherForecast> CreateAsync(WeatherForecast weatherForecast)
        {
            return await _weatherForecastRepository.CreateAsync(weatherForecast);
        }

        public async Task<int> DeleteAsync(DateOnly date)
        {
           return await _weatherForecastRepository.DeleteAsync(date);
        }

        public async Task<WeatherForecastDisplay> GetByDateAsync(DateOnly date)
        {
            var forecast= _weatherForecastRepository.GetByDateAsync(date).Result; 
            var dateForecast = new WeatherForecastDisplay() { TemperatureC= forecast.TemperatureC, Date= forecast.Date, Description= WeatherForecastConverter.GetWeatherCondition(forecast.TemperatureC) };

            return dateForecast;
        }

        public async Task<List<WeatherForecastDisplay>> GetWeekAsync(DateOnly date)
        {
            var week= _weatherForecastRepository.GetWeekAsync(date).Result;
            var weekForecast= new List<WeatherForecastDisplay>();
            for(int i=0; i< week.Count; i++)
            {
                
                weekForecast.Add( new WeatherForecastDisplay() { TemperatureC = week[i].TemperatureC, Date = week[i].Date, Description = WeatherForecastConverter.GetWeatherCondition(week[i].TemperatureC) });
            }
            return weekForecast;
        }

        public async Task<int> UpdateAsync(DateOnly date, WeatherForecast weatherForecast)
        {
            var tst = await (_weatherForecastRepository.UpdateAsync(date, weatherForecast));
            return tst;
        }
    }
}
