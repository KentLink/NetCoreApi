using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;
using Common.Utility.Log;

namespace MDES.Api
{
    public class BaseController : Controller
    {
        //public static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        //public IConfiguration _config;

        public BaseController()
        {
            LogAttribute.EndDate = null;
        }






        /// <summary>
        /// 自訂回傳
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiStatus"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Models.ApiResult<T> ThrowResult<T>(
        Enum.ApiStatusEnum apiStatus,
        string message, T data)
        {
            var apiResult = new Models.ApiResult<T>();

            if (apiStatus == Enum.ApiStatusEnum.OK)
            {
                apiResult.Status = Enum.ApiStatusEnum.OK;
                apiResult.Message = message;
                apiResult.Data = data;
                //LogUtility.Info();
            }
            else
            {
                apiResult.Status = apiStatus;
                //apiResult.Message = message;
                //throw new Exception(message);
            }

            if (apiStatus == Enum.ApiStatusEnum.InternalServerError)
            {
                LogUtility.Error(message);
                //logger.Error(message);
            }

            return apiResult;
        }

    }
}