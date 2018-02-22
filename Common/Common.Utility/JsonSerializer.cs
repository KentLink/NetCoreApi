using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;


namespace Common.Utility
{
    /// <summary>
    /// Json Object 序列化（反）序列化 Helper
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// 物件 2 json String
        /// </summary>
        /// <param name="obj">要序列化的物件</param>
        /// <returns></returns>
        public static string objToJsonString(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// json String 2 物件
        /// </summary>
        /// <typeparam name="T">要被反序列化的物件說明</typeparam>
        /// <param name="JasonString">要被反序列化的物件</param>
        /// <returns></returns>
        public static T JsonStringToObj<T>(string JasonString)
        {
            //HttpUtility.JavaScriptStringEncode()
            return JsonConvert.DeserializeObject<T>(EncodeJsonstring(JasonString) ?? "");

        }

        private static string EncodeJsonstring(string sourceStr)
        {
            return sourceStr;
        }

        public static ContentResult ToActionResult(this string jsonstring, bool isTextHtml = false)
        {
            string ContentType = "application/json";
            if (isTextHtml)
                ContentType = "text/html";
            return new ContentResult()
            {
                Content = jsonstring,
                ContentType = ContentType
                //ContentEncoding = Encoding.UTF8
            };
        }
    }
}

