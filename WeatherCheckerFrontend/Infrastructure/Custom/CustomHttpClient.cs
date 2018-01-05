using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherCheckerFrontend.Infrastructure.Custom.Interfaces;

namespace WeatherCheckerFrontend.Infrastructure.Custom
{
    public class CustomHttpClient : ICustomHttpClient
    {
        public async Task<HttpResponseMessage> GetAsync(Uri baseAddres,string requestUri)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddres;
            return await httpClient.GetAsync(requestUri);
        }
    }
}