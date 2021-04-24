using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Products.Application.ErrorHandler
{
    public class CustomException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }

        public CustomException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}
