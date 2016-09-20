using phoneword.rest.Actions;
using phoneword.rest.Filters;
using Phoneword.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace phoneword.rest.Controllers
{
    [ApiAuthenticationFilter]
    [RoutePrefix("api/auth")]
    public class AuthenticationController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(UserLogin user)
        {
            try
            {
                return Request.CreateResponse(AuthenticationAction.Login(user));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorMessage = ex.Message });
            }
        }

        [Route("login")]
        [HttpGet]
        public HttpResponseMessage TestGet(UserLogin user)
        {
            try
            {
                return Request.CreateResponse(AuthenticationAction.Login(user));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorMessage = ex.Message });
            }
        }
    }
}
