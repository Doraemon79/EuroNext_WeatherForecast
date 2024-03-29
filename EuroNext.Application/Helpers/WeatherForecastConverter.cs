﻿namespace EuroNext.Application.Helpers
{
    public static class WeatherForecastConverter
    {
        private static readonly IDictionary<string, (int, int)> _weatherConditions = new Dictionary<string, (int, int)>() {
            {"Freezing", (-60, -10) },
            {"Bracing", (-9, 0) },
            {"Chilli", (1, 5) },
            {"Cool", (6, 10) },
            {"Mild", (11, 15) },
            {"Warm", (16, 25) },
            {"Balmy", (26, 30) },
            {"Hot", (31, 40) },
            {"Sweltering", (41, 45) },
            {"Scorching", (46, 60) },
        };

        public static string GetWeatherCondition(int temperature)
        {
            var weatherCondtion = "Unknown";
            foreach (KeyValuePair<string, (int, int)> entry in _weatherConditions)
            {
                if (temperature >= entry.Value.Item1 && temperature <= entry.Value.Item2)
                {
                    weatherCondtion = entry.Key;
                    break;
                }
            }
            return weatherCondtion;
        }
    }
}
