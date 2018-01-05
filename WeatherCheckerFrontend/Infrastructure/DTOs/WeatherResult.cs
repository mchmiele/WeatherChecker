using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCheckerFrontend.Infrastructure.DTOs
{
    public class WeatherResult
    {
        public Location location { get; set; }

        public Temperature temperature { get; set; }

        public double humidity { get; set; }
    }

    public class Location
    {
        public string city { get; set; }

        public string country { get; set; }
    }

    public class Temperature
    {
        public string format { get; set; }
        public double value { get; set; }
    }
}