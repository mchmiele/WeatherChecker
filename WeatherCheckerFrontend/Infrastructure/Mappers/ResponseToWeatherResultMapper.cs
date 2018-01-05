using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using WeatherCheckerFrontend.Infrastructure.Mappers.Interfaces;

namespace WeatherCheckerFrontend.Infrastructure.Mappers
{
    public class ResponseToWeatherResultMapper : IResponseToWeatherResultMapper
    {
        public async Task<WeatherResult> Map(HttpResponseMessage message)
        {
            return await message.Content.ReadAsAsync<WeatherResult>();
        }
    }
}