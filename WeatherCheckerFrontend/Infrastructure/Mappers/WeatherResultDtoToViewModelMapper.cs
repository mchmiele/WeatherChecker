using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using WeatherCheckerFrontend.ViewModels;

namespace WeatherCheckerFrontend.Infrastructure.Mappers
{
    public class WeatherResultDtoToViewModelMapper
    {
        public static WeatherViewModel Map(WeatherResult weatherResult)
        {
            if (weatherResult?.location == null)
            {
                return new WeatherViewModel()
                {
                    IsSucceeded = false,
                    City = "",
                    Country = "",
                    Temperature = "0.0 C",
                    Humidity = "0.0"
                };
            }
            else
            {
                return new WeatherViewModel()
                {
                    IsSucceeded = true,
                    City = weatherResult.location.city,
                    Country = weatherResult.location.country,
                    Temperature = weatherResult.temperature.value.ToString("0.##") + " C",
                    Humidity = weatherResult.humidity.ToString("0.##")
                };
            }
        }

        public static WeatherViewModel EmptyWeatherViewModelWithMessage(string message)
        {
            return new WeatherViewModel()
            {
                IsSucceeded = false,
                Message = message,
                City = "",
                Country = "",
                Temperature = "0.0 C",
                Humidity = "0.0"
            };
        }
    }
}