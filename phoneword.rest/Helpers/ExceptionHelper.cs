using Phoneword.Services.ErrorHelper;
using Phoneword.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace phoneword.rest.Helpers
{
    public static class ExceptionHelper
    {
        private static ApiError CreateError(Exception ex)
        {
            if (ex is IApiException)
            {
                IApiException apiEx = ex as IApiException;
                return new ApiError { ErrorCode = apiEx.ErrorCode.ToString(), ErrorDescription = apiEx.ErrorDescription, HttpStatus = (int)apiEx.HttpStatus };
            }
            return new ApiError { ErrorCode = "999", ErrorDescription = ex.Message, HttpStatus = (int)HttpStatusCode.InternalServerError };
        }

        public static HttpResponseMessage CreateErrorResponse(this Exception ex, HttpRequestMessage request)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            if (ex is IApiException)
                statusCode = (ex as IApiException).HttpStatus;

            return request.CreateResponse(statusCode, CreateError(ex));
        }
    }
}
