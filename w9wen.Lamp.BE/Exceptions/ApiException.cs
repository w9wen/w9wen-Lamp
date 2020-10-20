using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace w9wen.Lamp.BE.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Connection { get; set; }

        public ApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            Connection = true;
            StatusCode = statusCode;
        }

        public ApiException(string message, bool connnection, Exception innerException)
            : base(message, innerException)
        {
            Connection = connnection;
            StatusCode = HttpStatusCode.ServiceUnavailable;
        }
    }

    public class ApiError
    {
        public string Message { get; set; }
    }
}