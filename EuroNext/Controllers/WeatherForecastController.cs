using Euronext.Domain.Entities;
using EuroNext.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<WeatherForecastDisplay>>> GetAll()
        {
            _logger.LogInformation("GetAll started");
            var forecasts = _euroNextService.GetWeekAsync(DateOnly.FromDateTime(DateTime.Now));
            return Ok(forecasts.Result);

        }

        [HttpGet("GetWeatherForecast/{date}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<WeatherForecastDisplay>> GetDetailsByDate( DateOnly date)
        {
            _logger.LogInformation("GetDetailsByDistrict started");
            var forecast = _euroNextService.GetByDateAsync(date);
            return Ok(forecast.Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="districtSalePerson"></param>
        /// <returns></returns>
        [HttpPost("WeatherForecast")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> InsertWeatherOfTheDay([FromBody] DateOnly date,  int temperature )
        {
           WeatherForecast weatherForecast=new WeatherForecast() { Date = date, TemperatureC=temperature };
            var createdForecast = await _euroNextService.CreateAsync(weatherForecast);
            //return CreatedAtAction(nameof(GetDetailsById), new { id = createdForecast.Date }, createdForecast);
            return Created();

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {

           var exist= await _euroNextService.UpdateAsync(weatherForecast.Date, weatherForecast);
            if (exist==0  )
            {
                return BadRequest();
            }
            return NoContent();

        }

        [HttpDelete("Remove/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteWeatherForecast(DateOnly date)
        {
            var exist = await _euroNextService.DeleteAsync(date);
            if (exist == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }


        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55)
        //    })
        //    .ToArray();
        //}

    }
}