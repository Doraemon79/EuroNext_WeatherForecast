using System.ComponentModel.DataAnnotations;

namespace Euronext.Domain.Entities
{
    public class WeatherForecast
    {
        [Required, Key]
        public DateOnly Date { get; set; }

        [Required]
        public int TemperatureC { get; set; }
    }
}
