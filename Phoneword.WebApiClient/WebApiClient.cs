using Phoneword.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.Util
{
    public class WebApiClient
    {
        private static string WebApiUrl;
        private static string WebCfgUrl = "http://phoneword-cfg.stor.sinaapp.com/";

        public string SessionToken { get; set; }

        public WebApiClient()
        {
            if (string.IsNullOrEmpty(WebApiUrl))
                LoadWebConfig();
        }

        private void LoadWebConfig()
        {
            DoGetConfig("config.json", new OnSuccessHandler(
                delegate (string resContent)
                {
                    AppConfig cfg = JSONUtil.ToObject<AppConfig>(resContent);
                    if (cfg != null && !string.IsNullOrWhiteSpace(cfg.WebApiUrl))
                        WebApiUrl = cfg.WebApiUrl;
                }
            ));
        }

        private void DoGetConfig(string requestUri, OnSuccessHandler success, OnFailureHandler failure = null)
        {
            WebRequest request = WebRequest.Create(WebCfgUrl + requestUri);
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = string.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    ApiError error = new ApiError { ErrorCode = "0", ErrorMessage = message };
                    failure?.Invoke(JSONUtil.ToJSONStr(error));
                }
                else
                {
                    string resContent = "{}";
                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                resContent = reader.ReadToEnd();
                            }
                        }
                        success?.Invoke(resContent);
                    }
                }
            }
        }

        public async Task DoGet(string requestUri, OnSuccessHandler success, OnFailureHandler failure = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUri);
                    string resContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        failure?.Invoke(resContent);
                    else
                        success?.Invoke(resContent);
                }
                catch (Exception ex)
                {
                    ApiError error = new ApiError { ErrorCode = "0", ErrorMessage = ex.Message };
                    failure?.Invoke(JSONUtil.ToJSONStr(error));
                }
            }
        }

        public async Task DoPost(string requestUri, object postData, OnSuccessHandler success, OnFailureHandler failure = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);
                try
                {
                    string sData = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
                    HttpContent content = new StringContent(sData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(requestUri, content);
                    string resContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        failure?.Invoke(resContent);
                    else
                        success?.Invoke(resContent);
                }
                catch (Exception ex)
                {
                    ApiError error = new ApiError { ErrorCode = "0", ErrorMessage = ex.Message };
                    failure?.Invoke(JSONUtil.ToJSONStr(error));
                }
            }
        }

        private void InitClient(HttpClient client)
        {
            client.BaseAddress = new Uri(WebApiUrl);
            if (!string.IsNullOrWhiteSpace(SessionToken))
                client.DefaultRequestHeaders.Add("authToken", SessionToken);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    public delegate void OnSuccessHandler(string resContent);
    public delegate void OnFailureHandler(string resContent);
}
