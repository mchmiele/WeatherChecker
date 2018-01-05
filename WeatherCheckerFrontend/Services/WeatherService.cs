using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherCheckerFrontend.Infrastructure.Custom;
using WeatherCheckerFrontend.Infrastructure.Custom.Interfaces;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using WeatherCheckerFrontend.Infrastructure.Exceptions;
using WeatherCheckerFrontend.Infrastructure.Mappers;
using WeatherCheckerFrontend.Infrastructure.Mappers.Interfaces;
using WeatherCheckerFrontend.Services.Interfaces;
using WeatherCheckerFrontend.ViewModels;

namespace WeatherCheckerFrontend.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ICustomHttpClient _httpClient;
        private readonly IResponseToWeatherResultMapper _responseToWeatherResultMapper;

        public WeatherService()
        {
            _httpClient = new CustomHttpClient();
            _responseToWeatherResultMapper = new ResponseToWeatherResultMapper();
        }

        public WeatherService(ICustomHttpClient httpClient,
            IResponseToWeatherResultMapper responseToWeatherResultMapper)
        {
            _httpClient = httpClient;
            _responseToWeatherResultMapper = responseToWeatherResultMapper;
        }

        public async Task<WeatherResult> GetWeather(string country, string city)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city))
            {
                throw new OneOfParametersIsEmptyException();
            }

            string apiWeatherUri = String.Format("/api/Weather/{0}/{1}", country, city);

            HttpResponseMessage response = await _httpClient.GetAsync(
                new Uri(System.Configuration.ConfigurationManager.AppSettings["WeatherWebApiAddress"]),
                apiWeatherUri);

            if (response.IsSuccessStatusCode)
            {
                WeatherResult weatherResult = await _responseToWeatherResultMapper.Map(response);

                if (weatherResult?.location == null)
                {
                    throw new InvalidParameters();
                }

                return weatherResult;

            }
            else
            {
                throw new WebApiInternalErrorException();
            }
        }
    }
}