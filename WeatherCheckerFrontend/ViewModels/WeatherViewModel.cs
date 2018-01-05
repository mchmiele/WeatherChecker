using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCheckerFrontend.ViewModels
{
    public class WeatherViewModel
    {
        public bool IsSucceeded { get; set; }

        public string Message { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Temperature { get; set; }

        public string Humidity { get; set; }
    }
}