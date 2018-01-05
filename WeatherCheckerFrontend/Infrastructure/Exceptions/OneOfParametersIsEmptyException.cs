using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherCheckerFrontend.Infrastructure.Exceptions
{
    public class OneOfParametersIsEmptyException : Exception
    {
        public OneOfParametersIsEmptyException()
        {
        }

        public OneOfParametersIsEmptyException(string message)
        : base(message)
        {
        }

        public OneOfParametersIsEmptyException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}