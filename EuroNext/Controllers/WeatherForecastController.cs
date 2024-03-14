using Euronext.Domain.Entities;
using EuroNext.Application.Services;
using Microsoft.AspNetCore.Mvc;
using static EuroNext.Validators.RequestValidator;

namespace EuroNext.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEuronextService _euroNextService;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEuronextService euroNextService)
        {
            _euroNextService = euroNextService;
            _logger = logger;
        }

        /// <summary>
        /// Shows the result of today and the next 7 days present
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeekWeatherForecast")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<WeatherForecastDisplay>>> GetAll()
        {

            _logger.LogInformation("GetAll started");
            var forecasts = _euroNextService.GetWeekAsync(DateOnly.FromDateTime(DateTime.Now));
            return Ok(forecasts.Result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("GetWeatherForecast/{date}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WeatherForecastDisplay>> GetDetailsByDate(DateOnly date)
        {
            _logger.LogInformation("GetDetailsByDistrict started");
            DateValidator validator = new DateValidator();
            var validationResult = validator.Validate(date);
            if (validationResult.IsValid)
            {
                var forecast = _euroNextService.GetByDateAsync(date);
                return Ok(forecast.Result);
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        [HttpPost("WeatherForecast")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> InsertWeatherOfTheDay([FromBody] DateOnly date, int temperature)
        {
            DateValidator validator = new DateValidator();
            var validationResult = validator.Validate(date);
            if (validationResult.IsValid)
            {
                WeatherForecast weatherForecast = new WeatherForecast() { Date = date, TemperatureC = temperature };
                await _euroNextService.CreateAsync(weatherForecast);
                return Created();
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weatherForecast"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> UpdateWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {

            ForecastValidator validator = new ForecastValidator();
            var validationResult = validator.Validate(weatherForecast);
            if (validationResult.IsValid)
            {
                var exist = await _euroNextService.UpdateAsync(weatherForecast.Date, weatherForecast);
                if (exist == 0)
                {
                    return BadRequest("No row has been updated");
                }
                return NoContent();
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpDelete("Remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> DeleteWeatherForecast(DateOnly date)
        {
            DateValidator validator = new DateValidator();
            var validationResult = validator.Validate(date);
            if (validationResult.IsValid)
            {
                var exist = await _euroNextService.DeleteAsync(date);
                if (exist == 0)
                {
                    return BadRequest("No row has been deleted");
                }
                return NoContent();
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

    }
}
