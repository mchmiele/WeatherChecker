using System;
using System.Dynamic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using WeatherCheckerApi.Serivces.Interfaces;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using WeatherCheckerApi.DTOs;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace WeatherCheckerApi.Serivces
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private const string WebApiKey = "87767ef45060de17bce88abd73a97b2e";
        private const string BaseUri = "http://api.openweathermap.org/data/2.5/";

        public async Task<WeatherResult> GetWeather(string country, string city)
        {
            var countryList = GetCountryList();

            if (!countryList.Keys.Contains(country))
            {
                return null;
            }

            var countryShortName = countryList[country];

            if (string.IsNullOrEmpty(countryShortName))
            {
                return null;
            }

            var resultJsonString = await GetWeatherJsonStringFromFeed(countryShortName, city);

            if (string.IsNullOrEmpty(resultJsonString))
            {
                return null;
            }

            dynamic deserializedResult = JsonConvert.DeserializeObject(resultJsonString);

            var temperatureDynamic = deserializedResult.main.temp;
            var humidityDynamic = deserializedResult.main.humidity;

            if (temperatureDynamic != null && humidityDynamic != null)
            {
                return new WeatherResult(country, city,
                    TemperatureFormatConverter.KelvinToCelcius(
                        Convert.ToDouble(temperatureDynamic.ToString())),
                    Convert.ToDouble(humidityDynamic.ToString()));
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetWeatherAsJson(string country, string city)
        {
            var weatherResult = await GetWeather(country, city);

            if (weatherResult != null)
            {
                return ConvertWeatherResultToJson(weatherResult);
            }
            else
            {
                return ConvertErrorMessageToJson(new ErrorMessage("Invalid Input parameters or server unavailability"));
            }
        }

        private async Task<string> GetWeatherJsonStringFromFeed(string country, string city)
        {
            HttpClient client = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 0, 10),
                BaseAddress = new Uri(BaseUri)
            };

            string weatherUri = String.Format("weather?q={0},{1}&appid={2}", city, country, WebApiKey);

            var resultJsonString = "";

            try
            {
                resultJsonString = await client.GetStringAsync(weatherUri);
            }
            catch (Exception)
            {
                resultJsonString = String.Empty;
            }

            return resultJsonString;
        }

        private string ConvertWeatherResultToJson(WeatherResult weatherResult)
        {
            return JsonConvert.SerializeObject(weatherResult);
        }

        private string ConvertErrorMessageToJson(ErrorMessage errorMessage)
        {
            return JsonConvert.SerializeObject(errorMessage);
        }

        public static Dictionary<string,string> GetCountryList()
        {
            Dictionary<string,string> cultureList = new Dictionary<string,string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    RegionInfo region = new RegionInfo(culture.LCID);

                    if (!(cultureList.ContainsKey(region.EnglishName)))
                        cultureList.Add(region.EnglishName,region.Name);
                }
                catch (Exception)
                {

                }
            }
            return cultureList;
        }
    }
}