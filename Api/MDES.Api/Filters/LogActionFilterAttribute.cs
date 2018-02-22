using Common.Utility.Log;
using MDES.Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MDES.Api.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private string name;

        public LogActionFilterAttribute() { }

        public LogActionFilterAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Action 前
        /// </summary>
        /// <param name="actionContext"></param>
        /// 
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            LogAttribute.StartDate =DateTime.Now;

            if (actionContext.ActionArguments.Count > 0)
            {
                LogAttribute.Id = actionContext.ActionDescriptor.Id;
                LogAttribute.DisplayName = actionContext.ActionDescriptor.DisplayName;

                //LogAttribute.Methods = JsonConvert.SerializeObject(actionContext.ActionDescriptor.ActionConstraints);
                LogAttribute.Methods = actionContext.HttpContext.Request.Method;
                LogAttribute.Controller = actionContext.ActionDescriptor.RouteValues["controller"];
                LogAttribute.Action = actionContext.ActionDescriptor.RouteValues["action"];
                LogAttribute.Url = actionContext.ActionDescriptor.AttributeRouteInfo.Template;
                LogAttribute.Body = JsonConvert.SerializeObject(actionContext.ActionArguments.Values);
                LogAttribute.Exception = "";
                string rawRequest = string.Empty;
                //LogManager.GetCurrentClassLogger().Info(
                //                    "Start" +
                //    JsonConvert.SerializeObject(actionContext.ActionDescriptor.DisplayName) + "\r\n" +
                //    JsonConvert.SerializeObject(actionContext.ActionDescriptor.Id) + "\r\n" +
                //    JsonConvert.SerializeObject(actionContext.ActionDescriptor.ActionConstraints) + "\r\n" +
                //    JsonConvert.SerializeObject(actionContext.ActionDescriptor.RouteValues) + "\r\n" +
                //    JsonConvert.SerializeObject(actionContext.ActionDescriptor.AttributeRouteInfo.Template) + "\r\n" +
                //    JsonConvert.SerializeObject(actionContext.ActionArguments.Values) 
                //    );

            }
            //if (actionContext.Request.Method.Method.ToLower() == "get")
            //{
            //    LogManager.GetCurrentClassLogger().Info("Get -- " + actionContext.Request.RequestUri + "\r\n" + rawRequest);
            //}
            //else
            //{
            //    if (actionContext.Request.Content.ReadAsStreamAsync().Result.Position != 0)
            //    {
            //        using (var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result))
            //        {
            //            stream.BaseStream.Position = 0;
            //            rawRequest = stream.ReadToEnd();
            //        }
            //        LogManager.GetCurrentClassLogger().Info("Post -- " + actionContext.Request.RequestUri + "\r\n" + rawRequest);
            //    }
            //}


        }

        /// <summary>
        /// Action 後
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogAttribute.EndDate = DateTime.Now;
            LogUtility.Info("ActionEnd");

            //LogManager.GetCurrentClassLogger().Info(
            //  JsonConvert.SerializeObject(context.ActionDescriptor.DisplayName) + "\r\n" +
            //  JsonConvert.SerializeObject(context.ActionDescriptor.Id) + "\r\n" +
            //  JsonConvert.SerializeObject(context.ActionDescriptor.ActionConstraints) + "\r\n" +
            //  JsonConvert.SerializeObject(context.ActionDescriptor.RouteValues) + "\r\n" +
            //  JsonConvert.SerializeObject(context.ActionDescriptor.AttributeRouteInfo.Template) + "\r\n" +
            //  "StartDate：" + StartDate.ToString() + "\r\n" + "EndDate：" + EndDate.ToString());


            //LogManager.GetCurrentClassLogger().Info(
            //    LogAttribute.StartDate.ToString() + "\t" +
            //    LogAttribute.EndDate.ToString() + "\t" +
            //    LogAttribute.Id + "\t" +
            //    LogAttribute.DisplayName + "\t" +
            //    LogAttribute.Methods + "\t" +
            //    LogAttribute.Controller + "\t" +
            //    LogAttribute.Action + "\t" +
            //    LogAttribute.Url + "\t" +
            //    LogAttribute.Body + "\t" +
            //    LogAttribute.Exception + "\t");



            //LogManager.GetCurrentClassLogger().Info(StartDate);
            //LogManager.GetCurrentClassLogger().Info(EndDate);

            //LogManager.GetCurrentClassLogger().Info("Action 後");
        }
        //public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        //{

        //}

        //public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        //{

        //}



    }
}