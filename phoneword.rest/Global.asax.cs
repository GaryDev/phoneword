using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace phoneword.rest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Path.Contains("api/auth/login")
                && !HttpContext.Current.Request.Headers.AllKeys.Contains("SessionToken"))
            {
                //HttpContext.Current.Response.StatusCode = 403;
                //(sender as HttpApplication).CompleteRequest();
            }
        }
    }
}
