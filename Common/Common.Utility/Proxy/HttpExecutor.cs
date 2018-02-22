using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Variable;
namespace Common.Utility.Proxy
{
    /// <summary>
    /// Http Action Helper(get,post))
    /// </summary>
    public static class HttpExecutor
    {

        private static readonly IDictionary<string, string> ContentTypes =
            new Dictionary<string, string>
            {
                { "Xml", "application/xml" },
                { "Json", "application/json" },
                { "Text", "text/plain" },
                { "x_www_form_urlencoded", "application/x-www-form-urlencoded" }
            };

        public static string GetContentTypesString(Variable.Enum.ContentTypeEnum contentType)
        {
            return ContentTypes[contentType.ToString()];
        }


        /// <summary>
        /// 執行http get
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public static HttpExecutorResult Get(string uri, Variable.Enum.ContentTypeEnum contentType)
        {
            HttpExecutorResult result = new HttpExecutorResult();
            try
            {
                using (WebClient client = new WebClient())
                {
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
                    req.Method = "GET";
                    req.Timeout = 30000;
                    req.ContentType = ContentTypes[contentType.ToString()];

                    using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            result.HttpStatus = HttpStatusCode.OK;
                            result.Data = sr.ReadToEnd();
                        }
                    }

                    return result;
                }
            }
            catch (WebException ex)
            {
                var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                result.HttpStatus = ((HttpWebResponse)ex.Response).StatusCode;
                result.Data = string.Empty;
                return result;
            }
        }

        public static HttpExecutorResult Get(string uri, Dictionary<HttpRequestHeader, string> httpHeaders)
        {
            HttpExecutorResult result = new HttpExecutorResult();
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;

                    foreach (KeyValuePair<HttpRequestHeader, string> item in httpHeaders)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }

                    result.HttpStatus = HttpStatusCode.OK;
                    result.Data = client.DownloadString(uri);
                    return result;
                }
            }
            catch (WebException ex)
            {
                var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                result.HttpStatus = ((HttpWebResponse)ex.Response).StatusCode;
                result.Data = string.Empty;
                return result;
            }
        }

        /// <summary>
        /// 執行http post
        /// </summary>
        /// <param name="uri">Url</param>
        /// <param name="Body">Body 資料內容</param>
        /// <param name="ContentType">傳輸格式</param>
        /// <returns></returns>
        public static HttpExecutorResult Post(string uri, string body, Variable.Enum.ContentTypeEnum contentType)
        {
            var httpHeader = new Dictionary<HttpRequestHeader, string>();
            httpHeader.Add(HttpRequestHeader.ContentType, ContentTypes[contentType.ToString()]);
            return Post(uri, body, httpHeader);
        }

        public static HttpExecutorResult xPost(string uri, string body, Dictionary<HttpRequestHeader, string> httpHeaders)
        {
            HttpExecutorResult result = new HttpExecutorResult();

            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(body);

                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 30000;
                request.ContentLength = postData.Length;
                foreach (var item in httpHeaders)
                {
                    if (item.Key != HttpRequestHeader.ContentType)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }


                }
                // 寫入 Post Body Message 資料流
                using (Stream st = request.GetRequestStream())
                {
                    st.Write(postData, 0, postData.Length);
                }

                // 取得回應資料
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result.Data = sr.ReadToEnd();
                        result.HttpStatus = HttpStatusCode.OK;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    result.HttpStatus = ((HttpWebResponse)ex.Response).StatusCode;
                }
                result.Data = string.Empty;
            }
            return result;
        }

        public static HttpExecutorResult Post(string uri, string body, Dictionary<HttpRequestHeader, string> httpHeaders)
        {
            HttpExecutorResult result = new HttpExecutorResult();
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    foreach (KeyValuePair<HttpRequestHeader, string> item in httpHeaders)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }

                    var response = client.UploadData(uri, "POST", Encoding.UTF8.GetBytes(body));

                    result.HttpStatus = HttpStatusCode.OK;
                    result.Data = Encoding.UTF8.GetString(response);
                    return result;

                }
            }
            catch (WebException ex)
            {

                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    result.HttpStatus = ((HttpWebResponse)ex.Response).StatusCode;
                }
                result.Data = string.Empty;
                return result;
            }
        }

        /// <summary>
        /// 裝載呼叫http Method的結果
        /// </summary>
        public struct HttpExecutorResult
        {
            /// <summary>
            /// 狀態（Http Status:200,404,500...） 
            /// </summary>
            public HttpStatusCode HttpStatus;
            /// <summary>
            /// 資料
            /// </summary>
            public string Data;
        }

        /// <summary>
        /// Wright--透過Client.uploadFile上傳檔案
        /// uploadFileName=上傳檔案名稱  
        /// uploadFileUrl = 上傳指定url
        /// fileAddress = 上傳檔案實體路徑
        /// </summary>
        /// <returns></returns>
        public static string HttpFileUploader(string uploadFileName, string uploadFileUrl, string fileAddress)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Content-Disposition", "attachment; filename=\"" + HttpUtility.UrlEncode(uploadFileName) + "\"");
            client.Headers.Add("Content-Type", "application/octet-stream");
            byte[] result = client.UploadFile(uploadFileUrl, "PUT", fileAddress);
            string thePutFileResult = Encoding.UTF8.GetString(result);

            return thePutFileResult;

        }


    }
}
