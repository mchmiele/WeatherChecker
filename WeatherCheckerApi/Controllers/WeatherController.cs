using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherCheckerApi.Serivces;
using WeatherCheckerApi.Serivces.Interfaces;

namespace WeatherCheckerApi.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly IOpenWeatherMapService _openWeatherMapService;

        public WeatherController()
        {
            _openWeatherMapService = new OpenWeatherMapService();
        }

        public WeatherController(IOpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
        }

        // GET: api/Weather/country/city
        public async Task<HttpResponseMessage> Get(string country, string city)
        {
            var weatherAsJsonString = await _openWeatherMapService.GetWeatherAsJson(country, city);

            return PrepareJsonResponse(weatherAsJsonString);
        }

        private HttpResponseMessage PrepareJsonResponse(string jsonString)
        {
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(jsonString)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return resp;
        }
    }
}
