using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Log
{
    public static class LogUtility
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public static void Message(string message = "",string type="")
        {
            LogAttribute.Message = message;
            logger.Info("[Message]\t" +
                type + "\t" +
                DateTime.Now.ToString() + "\t" + message + "\t");
        }

        public static void Info(string message = "")
        {
            LogAttribute.Message = message;
            logger.Info("[Info]\t" +
                LogAttribute.StartDate.ToString() + "\t" +
                (LogAttribute.EndDate == null ? "" : LogAttribute.EndDate.ToString()) + "\t" +
                LogAttribute.Id + "\t" +
                LogAttribute.DisplayName + "\t" +
                LogAttribute.Methods + "\t" +
                LogAttribute.Controller + "\t" +
                LogAttribute.Action + "\t" +
                LogAttribute.Url + "\t" +
                LogAttribute.Body + "\t" +
                LogAttribute.Message + "\t" +
                LogAttribute.Exception + "\t");
        }

        public static void Error(string exception, string message = "")
        {
            LogAttribute.Exception = exception;
            LogAttribute.Message = message;
            logger.Error("[Error]\t" + LogAttribute.StartDate.ToString() + "\t" +
                (LogAttribute.EndDate == null ? "" : LogAttribute.EndDate.ToString()) + "\t" +
                LogAttribute.Id + "\t" +
                LogAttribute.DisplayName + "\t" +
                LogAttribute.Methods + "\t" +
                LogAttribute.Controller + "\t" +
                LogAttribute.Action + "\t" +
                LogAttribute.Url + "\t" +
                LogAttribute.Body + "\t" +
                LogAttribute.Message + "\t" +
                LogAttribute.Exception + "\t");
        }

        public static void Warn(string message = "")
        {
            LogAttribute.Message = message;
            logger.Warn("[Warn]\t" + LogAttribute.StartDate.ToString() + "\t" +
                (LogAttribute.EndDate == null ? "" : LogAttribute.EndDate.ToString()) + "\t" +
                LogAttribute.Id + "\t" +
                LogAttribute.DisplayName + "\t" +
                LogAttribute.Methods + "\t" +
                LogAttribute.Controller + "\t" +
                LogAttribute.Action + "\t" +
                LogAttribute.Url + "\t" +
                LogAttribute.Body + "\t" +
                LogAttribute.Message + "\t" +
                LogAttribute.Exception + "\t");
        }

        public static void Debug(string exception, string message = "")
        {
            LogAttribute.Exception = exception;
            LogAttribute.Message = message;
            logger.Debug("[Debug]\t" + LogAttribute.StartDate.ToString() + "\t" +
                (LogAttribute.EndDate == null ? "" : LogAttribute.EndDate.ToString()) + "\t" +
                LogAttribute.Id + "\t" +
                LogAttribute.DisplayName + "\t" +
                LogAttribute.Methods + "\t" +
                LogAttribute.Controller + "\t" +
                LogAttribute.Action + "\t" +
                LogAttribute.Url + "\t" +
                LogAttribute.Body + "\t" +
                LogAttribute.Message + "\t" +
                LogAttribute.Exception + "\t");
        }

        public static void Trace(string message = "")
        {
            LogAttribute.Message = message;
            logger.Trace("[Trace]\t" + LogAttribute.StartDate.ToString() + "\t" +
                (LogAttribute.EndDate == null ? "" : LogAttribute.EndDate.ToString()) + "\t" +
                LogAttribute.Id + "\t" +
                LogAttribute.DisplayName + "\t" +
                LogAttribute.Methods + "\t" +
                LogAttribute.Controller + "\t" +
                LogAttribute.Action + "\t" +
                LogAttribute.Url + "\t" +
                LogAttribute.Body + "\t" +
                LogAttribute.Message + "\t" +
                LogAttribute.Exception + "\t");
        }

        public static void Fatal(string message = "")
        {
            LogAttribute.Message = message;
            logger.Fatal("[Fatal]\t" + LogAttribute.StartDate.ToString() + "\t" +
                (LogAttribute.EndDate == null ? "" : LogAttribute.EndDate.ToString()) + "\t" +
                LogAttribute.Id + "\t" +
                LogAttribute.DisplayName + "\t" +
                LogAttribute.Methods + "\t" +
                LogAttribute.Controller + "\t" +
                LogAttribute.Action + "\t" +
                LogAttribute.Url + "\t" +
                LogAttribute.Body + "\t" +
                LogAttribute.Message + "\t" +
                LogAttribute.Exception + "\t");
        }
    }
}
