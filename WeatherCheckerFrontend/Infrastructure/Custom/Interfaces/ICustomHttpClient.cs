using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherCheckerFrontend.Infrastructure.Custom.Interfaces
{
    public interface ICustomHttpClient
    {
        Task<HttpResponseMessage> GetAsync(Uri baseUri, string requestUri);
    }
}
