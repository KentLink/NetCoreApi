using Common.Utility.Log;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDES.Api.Filters
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception != null)
            {
                //LogManager.GetCurrentClassLogger().Error(exceptionContext.Exception);
                //LogManager.GetCurrentClassLogger().Error(
                //    JsonConvert.SerializeObject(exceptionContext.ActionDescriptor.DisplayName) + "\r\n" +
                //    JsonConvert.SerializeObject(exceptionContext.ActionDescriptor.Id) + "\r\n" +
                //    JsonConvert.SerializeObject(exceptionContext.ActionDescriptor.ActionConstraints) + "\r\n" +
                //    JsonConvert.SerializeObject(exceptionContext.ActionDescriptor.RouteValues) + "\r\n" +
                //    JsonConvert.SerializeObject(exceptionContext.ActionDescriptor.AttributeRouteInfo.Template) + "\r\n" +
                //    JsonConvert.SerializeObject(exceptionContext.Exception)
                //    );

                LogUtility.Error(exceptionContext.Exception.ToString(), "ActionError");
            }

            //base.OnException(actionExecutedContext);
        }

    }

}