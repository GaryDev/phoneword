using phoneword.rest.Filters;
using Phoneword.Services;
using Phoneword.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace phoneword.rest.Controllers
{
    [ApiAuthenticationFilter]
    [RoutePrefix("api/v1/auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ITokenService _tokenService;
        private static readonly string _authTokenExpiry = ConfigurationManager.AppSettings["AuthTokenExpiry"];

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        #endregion

        [HttpPost]
        //[Route(Name = "authenticate", Order = 1)]
        //[Route(Name = "login", Order = 2)]
        //[Route(Name = "get/token", Order = 3)]
        [Route("login")]
        public HttpResponseMessage Authenticate()
        {
            try
            {
                if (System.Threading.Thread.CurrentPrincipal != null 
                    && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                    {
                        var userId = basicAuthenticationIdentity.UserId;
                        return GetAuthToken(userId);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new ApiError { ErrorCode = "401", ErrorDescription = "Unauthorized" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError { ErrorCode = "500", ErrorDescription = ex.Message });
            }
        }

        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private HttpResponseMessage GetAuthToken(int userId)
        {
            var token = _tokenService.GenerateToken(userId);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", _authTokenExpiry);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }        
    }
}
