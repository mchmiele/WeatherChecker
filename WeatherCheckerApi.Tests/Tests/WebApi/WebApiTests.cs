using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherCheckerApi.Serivces.Interfaces;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using Xunit;

namespace WeatherCheckerApi.Tests.Tests.WebApi
{
    public class WebApiTests
    {
        [Theory]
        [InlineData("Poland","Warsaw")]
        public async Task WebApiGetWeather_ProperParameters_ResponseOkFromTheServer(string country, string city)
        {
            var WebApiUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["WeatherWebApiAddress"]);

            string apiWeatherUri = String.Format("/api/Weather/{0}/{1}", country, city);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = WebApiUri;
            var result = await httpClient.GetAsync(apiWeatherUri);

            Assert.True(result.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("Poland", "Warsaw")]
        public async Task WebApiGetWeather_ProperParameters_ReasonableJsonInResponseFromTheServer(string country, string city)
        {
            var WebApiUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["WeatherWebApiAddress"]);

            string apiWeatherUri = String.Format("/api/Weather/{0}/{1}", country, city);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = WebApiUri;
            var result = await httpClient.GetAsync(apiWeatherUri);

            WeatherResult weatherResult = await result.Content.ReadAsAsync<WeatherResult>();

            Assert.True(weatherResult?.location != null && weatherResult.location.city == city
                && weatherResult.location.country == country);
            Assert.True(weatherResult.temperature != null);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("One", "")]
        [InlineData("", "Two")]
        [InlineData("One", "Two")]
        public async Task WebApiGetWeather_InvalidParameters_EmptyResponse(string country, string city)
        {
            var WebApiUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["WeatherWebApiAddress"]);

            string apiWeatherUri = String.Format("/api/Weather/{0}/{1}", country, city);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = WebApiUri;
            var result = await httpClient.GetAsync(apiWeatherUri);

            WeatherResult weatherResult = await result.Content.ReadAsAsync<WeatherResult>();

            Assert.True(weatherResult.location == null);
        }

    }
}
