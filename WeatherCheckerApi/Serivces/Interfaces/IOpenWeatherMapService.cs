using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherCheckerApi.DTOs;

namespace WeatherCheckerApi.Serivces.Interfaces
{
    public interface IOpenWeatherMapService
    {
        Task<WeatherResult> GetWeather(string country, string city);
        Task<string> GetWeatherAsJson(string country, string city);
    }
}
