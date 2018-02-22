using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Log
{
    /// <summary>
    /// Log 參數 屬性
    /// </summary>
    public class LogAttribute
    {
        public static DateTime StartDate;
        public static DateTime? EndDate;
        public static string Id;
        public static string DisplayName;
        public static string Methods;
        public static string Controller;
        public static string Action;
        public static string Url;
        public static string Body;
        public static string Exception;
        public static string Message;
    }
}
