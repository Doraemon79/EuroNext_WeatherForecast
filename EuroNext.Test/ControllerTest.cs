using Euronext.Domain.Entities;
using EuroNext.Application.Services;
using EuroNext.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EuroNext.Test
{
    public class ControllerTest
    {
        WeatherForecastDisplay forecast1 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 13), TemperatureC = 50, Description= "Scorching" };
        WeatherForecastDisplay forecast2 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 14), TemperatureC = 42, Description = "Sweltering" };
        WeatherForecastDisplay forecast3 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 15), TemperatureC = 27, Description = "Balmy" };
        WeatherForecastDisplay forecast4 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 16), TemperatureC = 20, Description = "Warm" };
        WeatherForecastDisplay forecast5 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 17), TemperatureC = 0, Description = "Bracing" };
        WeatherForecastDisplay forecast6 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 18), TemperatureC = -10, Description = "Freezing" };
        WeatherForecastDisplay forecast7 = new WeatherForecastDisplay() { Date = new DateOnly(2024, 3, 19), TemperatureC = -30, Description = "Freezing" };


        [Fact]
        public async Task TestGetByDateReturnsOk()
        {
            // Arrange
            var euroNextServiceMock = new Mock<IEuronextService>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();

            ObservableCollection<WeatherForecastDisplay> forecasts = new ObservableCollection<WeatherForecastDisplay>() { forecast1 , forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };

            // Act
            DateOnly date = new DateOnly(2024,3,13);
     
            euroNextServiceMock.Setup(r => r.GetByDateAsync(date))
                .Returns(Task.FromResult(forecasts.First() )) ;
            var controller = new WeatherForecastController(loggerMock.Object, euroNextServiceMock.Object);
            var result = await controller.GetDetailsByDate(date);

           //Assert
           Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task TestGetAllReturnsOk()
        {
            // Arrange
            var euroNextServiceMock = new Mock<IEuronextService>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();

            ObservableCollection<WeatherForecastDisplay> forecasts = new ObservableCollection<WeatherForecastDisplay>() { forecast1, forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };

            // Act
            DateOnly date = new DateOnly(2024, 3, 13);

            euroNextServiceMock.Setup(r => r.GetWeekAsync(date))
                .Returns(Task.FromResult(forecasts.ToList()));
            var controller = new WeatherForecastController(loggerMock.Object, euroNextServiceMock.Object);
            var result = await controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task TestCreateReturnsCreated()
        {
            // Arrange
            var euroNextServiceMock = new Mock<IEuronextService>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();

            ObservableCollection<WeatherForecastDisplay> forecasts = new ObservableCollection<WeatherForecastDisplay>() { forecast1, forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };

            WeatherForecast forecast = new WeatherForecast() { Date = new DateOnly(2024, 3, 13), TemperatureC = 20 };
            DateOnly Date = new DateOnly(2024, 3, 13);

            // Act
            DateOnly date = new DateOnly(2024, 3, 13);

            euroNextServiceMock.Setup(r => r.CreateAsync(forecast))
                .Returns(Task.FromResult(forecast));
            var controller = new WeatherForecastController(loggerMock.Object, euroNextServiceMock.Object);
            var result = await controller.InsertWeatherOfTheDay(Date, 20);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task TestUpdateReturnsNoResult()
        {
            // Arrange
            var euroNextServiceMock = new Mock<IEuronextService>();
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();

            //ObservableCollection<WeatherForecastDisplay> forecasts = new ObservableCollection<WeatherForecastDisplay>() { forecast1, forecast2, forecast3, forecast4, forecast5, forecast6, forecast7 };
            WeatherForecast forecast = new WeatherForecast() { Date = new DateOnly(2024, 3, 13), TemperatureC = 20 };
            DateOnly date = new DateOnly(2024, 3, 13);
           euroNextServiceMock.Setup(r => r.UpdateAsync(date, forecast) ).ReturnsAsync(1);

            // Act 
            var controller = new WeatherForecastController(loggerMock.Object, euroNextServiceMock.Object);
            var result = await controller.UpdateWeatherForecast(forecast);

            //Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}