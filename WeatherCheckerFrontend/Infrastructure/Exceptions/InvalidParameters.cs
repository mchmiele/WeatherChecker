using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCheckerFrontend.Infrastructure.Exceptions
{
    public class InvalidParameters : Exception
    {
        public InvalidParameters()
        {
        }

        public InvalidParameters(string message)
        : base(message)
        {
        }

        public InvalidParameters(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}