using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCheckerApi.Serivces
{
    public static class TemperatureFormatConverter
    {
        public static double FereinheitToCelcius(double ferenheitDegrees)
        {
            double celciusDegrees = 5.0 / 9.0 * (ferenheitDegrees - 32);

            return celciusDegrees;
        }

        public static double KelvinToCelcius(double kelvinDegrees)
        {
            return kelvinDegrees - 273.15;
        }
    }
}