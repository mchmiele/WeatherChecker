using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.Extensions;
using WeatherCheckerFrontend.Infrastructure.Exceptions;
using WeatherCheckerFrontend.Services;
using WeatherCheckerFrontend.Services.Interfaces;
using Xunit;
using System.Net.Http;
using WeatherCheckerFrontend.Infrastructure.Custom.Interfaces;
using WeatherCheckerFrontend.Infrastructure.Mappers;
using WeatherCheckerFrontend.Infrastructure.Mappers.Interfaces;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using WeatherCheckerFrontend.Infrastructure.Custom;

namespace WeatherCheckerFrontend.Tests.Tests.WeatherServiceTests
{
    public class WeatherServiceTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Poland","")]
        [InlineData("", "Warsaw")]
        public async Task GetWeather_EmptyParameters_OneOfParametersIsEmptyExceptionThrown(
            string country, string city)
        {
            IWeatherService weatherService = new WeatherService();
            await Assert.ThrowsAsync<OneOfParametersIsEmptyException>(
                (async () => await weatherService.GetWeather(country, city)));
        }

        [Theory]
        [InlineData("Poland", "Tomorrowland")]
        [InlineData("Tomorrowland", "Warsaw")]
        [InlineData("Tomorrowland", "Tomorrowland")]
        public async Task GetWeather_InvalidParameters_OneOfParametersIsInvalidExceptionThrown(
            string country, string city)
        {
            IWeatherService weatherService = new WeatherService();
            await Assert.ThrowsAsync<InvalidParameters>(
                (async () => await weatherService.GetWeather(country, city)));
        }

        [Fact]
        public async Task GetWeather_WebApiError_ResultNull()
        {
            string country = "Poland";
            string city = "Warsaw";

            HttpResponseMessage failResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            ICustomHttpClient httpClient = Substitute.For<ICustomHttpClient>();
            httpClient.GetAsync(Arg.Any<Uri>(),Arg.Any<string>()).Returns(failResponseMessage);

            var weatherService = new WeatherService(httpClient, new ResponseToWeatherResultMapper());

            await Assert.ThrowsAsync<WebApiInternalErrorException>(
                (async () => await weatherService.GetWeather(country, city)));
        }

        [Fact]
        public async Task GetWeather_Success_ResultWeatherResult()
        {
            string country = "Poland";
            string city = "Warsaw";

            HttpResponseMessage failResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            IResponseToWeatherResultMapper responseToWeatherResultMapper =
                Substitute.For<IResponseToWeatherResultMapper>();

            WeatherResult expectedWeatherResult = new WeatherResult()
            {
                location = new Location() { country = country, city = city },
                temperature = new Temperature() { format = "Celsius", value = 40.1},
                humidity = 52.1
            };

            responseToWeatherResultMapper.Map(Arg.Any<HttpResponseMessage>()).Returns(expectedWeatherResult);

            ICustomHttpClient httpClient = Substitute.For<ICustomHttpClient>();
            httpClient.GetAsync(Arg.Any<Uri>(), Arg.Any<string>()).Returns(failResponseMessage);

            var weatherService = new WeatherService(httpClient, responseToWeatherResultMapper);
            var weatherResult = await weatherService.GetWeather(country, city);

            Assert.Equal(expectedWeatherResult, weatherResult);
        }
    }
}
