using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace phoneword.rest.ErrorHelper
{
    public class ApiDataException : Exception, IApiException
    {
        private string _reasonPhase = "ApiDataException";

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public HttpStatusCode HttpStatus { get; set; }

        public string ReasonPhrase
        {
            get { return _reasonPhase; }
            set { _reasonPhase = value; }
        }

        public ApiDataException(int errorCode, string errorDescription, HttpStatusCode httpStatus)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            HttpStatus = httpStatus;
        }
    }
}