using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherCheckerFrontend.Infrastructure.DTOs;

namespace WeatherCheckerFrontend.Infrastructure.Mappers.Interfaces
{
    public interface IResponseToWeatherResultMapper
    {
        Task<WeatherResult> Map(HttpResponseMessage message);
    }
}
