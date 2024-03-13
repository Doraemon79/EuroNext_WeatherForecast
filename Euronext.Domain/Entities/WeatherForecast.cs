using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
