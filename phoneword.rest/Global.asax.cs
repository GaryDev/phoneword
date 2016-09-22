using Phoneword.ViewModel;
using Phoneword.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace phoneword.rest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private const int DATE_RANGE = 900; // Seconds
        private const string DUMMY_PASSWORD = "hackjack";

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //ApiError validateResult = ValidateAuthentication(HttpContext.Current.Request);
            //if (validateResult != null)
            //{
            //    HttpContext.Current.Response.StatusCode = int.Parse(validateResult.ErrorCode);
            //    (sender as HttpApplication).CompleteRequest();
            //}
        }

        private ApiError ValidateAuthentication(HttpRequest request)
        {
            string requestToken = string.Empty;
            string requestDate = string.Empty;
            string requestParameter = string.Empty;

            if (request.HttpMethod.ToUpper() == "GET")
            {
                NameValueCollection parameters = request.QueryString;
                requestParameter = EncryptUtil.CreateSortedParams(parameters);
            }
            else
            {
                string parameter = new System.IO.StreamReader(request.InputStream).ReadToEnd();
                requestParameter = JSONUtil.NormalizeJsonString(parameter);
            }

            NameValueCollection headers = request.Headers;
            if (headers.AllKeys.Contains("HTTP_X_AUTHORIZATION"))
                requestToken = headers["HTTP_X_AUTHORIZATION"];

            if (headers.AllKeys.Contains("HTTP_X_DATE"))
                requestDate = headers["HTTP_X_DATE"];

            if (string.IsNullOrWhiteSpace(requestDate) || !ValidateDate(requestDate))
                return new ApiError { ErrorCode = "403", ErrorDescription = "Date is invalid" };

            if (string.IsNullOrWhiteSpace(requestToken) || 
                !ValidateToken(requestToken, requestDate, request.HttpMethod, requestParameter))
                return new ApiError { ErrorCode = "401", ErrorDescription = "Unauthorized" };

            return null;
        }

        private bool ValidateDate(string date)
        {
            DateTime clientTime;
            if (!DateTime.TryParseExact(date, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out clientTime))
                return false;
            
            DateTime serverTime = DateTime.UtcNow;
            DateTime minDateTime = serverTime.AddSeconds(-DATE_RANGE);
            DateTime maxDateTime = serverTime.AddSeconds(DATE_RANGE);

            if (clientTime > minDateTime && clientTime < maxDateTime)
                return true;

            return false;
        }

        private bool ValidateToken(string token, string date, string method = null, string paramString = null)
        {
            string[] tokens = token.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens != null && tokens.Length == 2)
            {
                string username = tokens[0];
                string requestSignature = tokens[1];
                string password = DUMMY_PASSWORD;
                string httpMethod = string.IsNullOrWhiteSpace(method) ? "GET" : method.ToUpper();

                StringBuilder sb = new StringBuilder();
                sb.Append(httpMethod).Append("\n")
                  .Append(paramString).Append("\n")
                  .Append(date).Append("\n");
                string strToSign = sb.ToString() + password;

                string signature = EncryptUtil.CreateSignature(strToSign, SignatureMethod.MD5);
                if (requestSignature == signature)
                    return true;
            }
            return false;
        }
    }
}
