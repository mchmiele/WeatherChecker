using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WeatherCheckerFrontend.Infrastructure.DTOs;
using WeatherCheckerFrontend.Infrastructure.Exceptions;
using WeatherCheckerFrontend.Infrastructure.Mappers;
using WeatherCheckerFrontend.Services;
using WeatherCheckerFrontend.Services.Interfaces;
using WeatherCheckerFrontend.ViewModels;

namespace WeatherCheckerFrontend.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController()
        {
            _weatherService = new WeatherService();
        }

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public ActionResult Check()
        {
            return View();
        }

        public async Task<JsonResult> CheckWeather(string country, string city)
        {
            WeatherViewModel viewModel;

            try
            {
                viewModel = WeatherResultDtoToViewModelMapper
                    .Map(await _weatherService.GetWeather(country, city));
            }
            catch (OneOfParametersIsEmptyException)
            {
                viewModel =
                    WeatherResultDtoToViewModelMapper
                    .EmptyWeatherViewModelWithMessage("One of parameters is empty");
            }
            catch (WebApiInternalErrorException)
            {
                viewModel =
                    WeatherResultDtoToViewModelMapper.EmptyWeatherViewModelWithMessage(
                        "Server with weather is unvailable");
            }
            catch (InvalidParameters)
            {
                viewModel =
                    WeatherResultDtoToViewModelMapper.EmptyWeatherViewModelWithMessage(
                        "Invalid parameters in form");
            }

            return Json(new { WeatherResult = viewModel }, JsonRequestBehavior.AllowGet);
        }
    }
}