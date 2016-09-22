using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Phoneword.Services.ErrorHelper
{
    public class ApiException : Exception, IApiException
    {
        private string _reasonPhase = "ApiException";

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public HttpStatusCode HttpStatus { get; set; }

        public string ReasonPhrase
        {
            get { return _reasonPhase; }
            set { _reasonPhase = value; }
        }
    }
}