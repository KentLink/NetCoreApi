using MDES.Api.Model.Result;
using System.Collections.Generic;
using System.Net;

namespace Common.Utility.Proxy
{
    /// <summary>
    /// 實作Web call Api
    /// </summary>
    public class ProxyHelper
    {
        public void ProxyPostAction<DataT>(string url,
            string body, Variable.Enum.ContentTypeEnum contentType, ref ApiResult<DataT> apiResult)
        {
            var httpHeaders = new Dictionary<HttpRequestHeader, string>();
            httpHeaders.Add(HttpRequestHeader.ContentType, HttpExecutor.GetContentTypesString(Variable.Enum.ContentTypeEnum.Json));
            //httpHeaders.Add(HttpRequestHeader.Authorization, Variable.FakeToken);
            var httpExecutorResult = HttpExecutor.xPost(url, body, httpHeaders);
            this.ProcessResult<DataT>(httpExecutorResult, ref apiResult);
        }




        public void ProcessResult<T>(HttpExecutor.HttpExecutorResult httpExecutorResult, ref ApiResult<T> apiResult)
        {
            switch (httpExecutorResult.HttpStatus)
            {
                case HttpStatusCode.InternalServerError:
                    apiResult.ApiStatus = Variable.Enum.ApiStatusEnum.InternalServerError;
                    apiResult.Message = Variable.Enum.GetApiStatuString(Variable.Enum.ApiStatusEnum.InternalServerError);
                    break;
                case HttpStatusCode.NotFound:
                    apiResult.ApiStatus = Variable.Enum.ApiStatusEnum.NotFound;
                    apiResult.Message = Variable.Enum.GetApiStatuString(Variable.Enum.ApiStatusEnum.NotFound);
                    break;
                case HttpStatusCode.OK:
                    apiResult = Common.Utility.JsonSerializer.JsonStringToObj<ApiResult<T>>
                    (httpExecutorResult.Data);

                    if (string.IsNullOrEmpty(apiResult.Message) == true)
                    {
                        apiResult.ApiStatus = Variable.Enum.ApiStatusEnum.OK;
                    }
                    else
                    {
                        apiResult.ApiStatus = Variable.Enum.ApiStatusEnum.Customer;
                    }

                    break;
                case HttpStatusCode.Unauthorized:
                    apiResult.ApiStatus = Variable.Enum.ApiStatusEnum.Unauthorized;
                    apiResult.Message = Variable.Enum.GetApiStatuString(Variable.Enum.ApiStatusEnum.Unauthorized);
                    break;
                default:
                    apiResult.ApiStatus = Variable.Enum.ApiStatusEnum.UnKnow;
                    apiResult.Message = Variable.Enum.GetApiStatuString(Variable.Enum.ApiStatusEnum.UnKnow);
                    break;
            }
        }
    }
}
