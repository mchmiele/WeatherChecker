using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using WeatherCheckerFrontend.ViewModels;

namespace WeatherCheckerFrontend.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetWeather(string country, string city);
    }
}
