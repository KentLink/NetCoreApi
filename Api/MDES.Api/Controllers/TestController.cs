using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utility.Log;
using MDES.Api.Model.Arg;
using MDES.Api.Model.Test;
using MDES.Api.Models;
using MDES.Api.Server.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace MDES.Api.Controllers
{
    /// <summary>
    /// 測試 Controller
    /// </summary>
    /// <threadsafety static="true" instance="true" />
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : BaseController
    {
        /// <summary>
        /// 測試Http Get Case
        /// </summary>
        /// <param name="arg">參數</param>
        /// <returns>我是回傳</returns>
        [Filters.LogActionFilterAttribute()]
        [Filters.LogExceptionFilterAttribute]
        [Route("CaseError")]
        [HttpGet]
        public IActionResult Case(TestArg arg)
        {
            try
            {

                //for (int i = 0; i < 20; i++)
                //{
                //    Thread.Sleep(1000);
                //    LogUtility.Info(i.ToString());
                //    //Console.WriteLine(i.ToString());
                //}

                //logger.Error("TT");
                //logger.Info(defaultCulture);
                //logger.Warn(defaultCulture);
                //logger.Debug(defaultCulture);
                //throw new ExecutionEngineException();
                TestService testService = new TestService();
                List<TestResultModel> result = testService.GetTestSqlData(arg);

                return Ok(this.ThrowResult<List<TestResultModel>>(Enum.ApiStatusEnum.OK, "", result));
            }
            catch (Exception ex)
            {
                return Ok(this.ThrowResult<string>(Enum.ApiStatusEnum.InternalServerError, ex.ToString(), string.Empty));


            }

        }

        /// <summary>
        /// 測試Http Proxy Call Api Case
        /// </summary>
        /// <param name="arg">參數</param>
        /// <returns>我是回傳</returns>
        [Filters.LogActionFilterAttribute()]
        [Filters.LogExceptionFilterAttribute]
        [Route("CaseProxy")]
        [HttpGet]
        public IActionResult GetTestProxyData(TestArg arg)
        {

            try
            {
                TestService testService = new TestService();
                List<TestResultModel> result = testService.GetTestProxyData(arg);

                return Ok(this.ThrowResult<List<TestResultModel>>(Enum.ApiStatusEnum.OK, "", result));
            }
            catch (Exception ex)
            {
                return Ok(this.ThrowResult<string>(Enum.ApiStatusEnum.InternalServerError, ex.ToString(), string.Empty));


            }
        }

        /// <summary>
        /// 測試Http Post Case
        /// </summary>
        /// <param name="arg">參數</param>
        /// <returns>我是回傳</returns>
        [Filters.LogActionFilterAttribute()]
        [Filters.LogExceptionFilterAttribute]
        [Route("CasePost")]
        [HttpPost]
        public IActionResult CasePost(TestArg arg)
        {
            try
            {
                TestService testService = new TestService();
                List<TestResultModel> result = testService.GetTestSqlData(arg);

                return Ok(this.ThrowResult<List<TestResultModel>>(Enum.ApiStatusEnum.OK, "", result));
            }
            catch (Exception ex)
            {
                return Ok(this.ThrowResult<string>(Enum.ApiStatusEnum.InternalServerError, ex.ToString(), string.Empty));


            }

        }



    }
}