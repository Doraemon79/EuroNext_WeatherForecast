using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euronext.Domain.Entities
{
    public class WeatherForecastDisplay
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }

        public string Description { get; set;}
    }
}
