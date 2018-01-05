using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCheckerFrontend.Infrastructure.Exceptions
{
    public class WebApiInternalErrorException : Exception
    {
        public WebApiInternalErrorException()
        {
        }

        public WebApiInternalErrorException(string message)
        : base(message)
        {
        }

        public WebApiInternalErrorException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}