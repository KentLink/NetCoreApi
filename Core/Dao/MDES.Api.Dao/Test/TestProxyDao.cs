using Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Variable;
using MDES.Api.Model.Test;
using MDES.Api.Model.Arg;
using MDES.Api.Model.Result;

namespace MDES.Api.Dao.Test
{
    public class TestProxyDao : ProxyBase
    {
        /// <summary>
        /// Test
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<TestResultModel> GetTestProxyData(TestArg arg)
        {

            string apiUrl = URLVariable.ApiRootUrl + URLVariable.TestData;
            apiUrl = "https://demos.telerik.com/kendo-ui/service/Products";
            var apiResult = new ApiResult<List<TestResultModel>>();
            this.porxyHelper.ProxyPostAction<List<TestResultModel>>
                (apiUrl, JsonSerializer.objToJsonString(arg), Variable.Enum.ContentTypeEnum.Json, ref apiResult);
            return apiResult.Data;
        }

    }
}
