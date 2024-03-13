using Euronext.Domain.Entities;
using Euronext.Domain.Repository;
using EuroNext.Application.Services;
using EuroNext.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroNext.Test
{
    public class EuronextServiceTest
    {
        WeatherForecast forecast1 = new WeatherForecast() { Date = new DateOnly(2024, 3, 13), TemperatureC = 50};
        WeatherForecast forecast2 = new WeatherForecast() { Date = new DateOnly(2024, 3, 14), TemperatureC = 42 };
        WeatherForecast forecast3 = new WeatherForecast() { Date = new DateOnly(2024, 3, 15), TemperatureC = 27 };
        WeatherForecast forecast4 = new WeatherForecast() { Date = new DateOnly(2024, 3, 16), TemperatureC = 20};
        WeatherForecast forecast5 = new WeatherForecast() { Date = new DateOnly(2024, 3, 17), TemperatureC = 0 };
        WeatherForecast forecast6 = new WeatherForecast() { Date = new DateOnly(2024, 3, 18), TemperatureC = -10 };
        WeatherForecast forecast7 = new WeatherForecast() { Date = new DateOnly(2024, 3, 19), TemperatureC = -30 };
       
        [Fact]
        public async Task TestGetByDateReturnsRightDescription()
        {
            // Arrange
            var WeatherForecastRepositoryMock = new Mock<IWeatherForecastRepository>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();
            ObservableCollection<WeatherForecast> forecasts = new ObservableCollection<WeatherForecast>() { forecast1, forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };
            DateOnly date = new DateOnly(2024, 3, 13);

            // Act
            var service = new EuronextService(WeatherForecastRepositoryMock.Object);
            WeatherForecastRepositoryMock.Setup(r => r.GetByDateAsync(date)).Returns(Task.FromResult(forecast1));
            var forecast = await service.GetByDateAsync(date);

            //Assert
            Assert.Equal("Scorching", forecast.Description);
        }

        [Fact]
        public async Task TestGetWeekReturnsRightDescription()
        {
            // Arrange
            var WeatherForecastRepositoryMock = new Mock<IWeatherForecastRepository>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();
            ObservableCollection<WeatherForecast> forecasts = new ObservableCollection<WeatherForecast>() { forecast1, forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };
            DateOnly date = new DateOnly(2024, 3, 13);

            // Act
            var service = new EuronextService(WeatherForecastRepositoryMock.Object);
            WeatherForecastRepositoryMock.Setup(r => r.GetWeekAsync(date)).Returns(Task.FromResult(forecasts.ToList()));
            var forecast = await service.GetWeekAsync(date);

            //Assert
            Assert.Equal("Scorching", forecast.First().Description);
            Assert.Equal("Sweltering", forecast.ElementAt(1).Description);
        }

        [Fact]
        public async Task TestUpdateReturnsRightDescription()
        {
            // Arrange
            var WeatherForecastRepositoryMock = new Mock<IWeatherForecastRepository>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();
            ObservableCollection<WeatherForecast> forecasts = new ObservableCollection<WeatherForecast>() { forecast1, forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };
            DateOnly date = new DateOnly(2024, 3, 13);
            WeatherForecast forecastTst = new WeatherForecast() { Date = new DateOnly(2024, 3, 13), TemperatureC = 20 };

            // Act
            var service = new EuronextService(WeatherForecastRepositoryMock.Object);
            WeatherForecastRepositoryMock.Setup(r => r.UpdateAsync(date, forecastTst)).Returns(Task.FromResult(1));
            var result = await service.UpdateAsync(date, forecastTst);

            //Assert
            Assert.Equal(1, result); 
        }

    }
}
