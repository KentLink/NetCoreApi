using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variable
{
    public class Enum
    {
        /// <summary>
        /// Http ContentType
        /// </summary>
        public enum ContentTypeEnum
        {
            Xml,
            Json,
            Text,
            x_www_form_urlencoded
        }
        /// <summary>
        /// 計錄Log時的要指定的狀態
        /// </summary>
        public enum LogCategoryEnum
        {
            Information,
            Error,
            Warning
        }

        /// <summary>
        /// Api 回傳狀態
        /// </summary>
        public enum ApiStatusEnum
        {
            OK,
            InternalServerError,
            Unauthorized,
            NotFound,
            Customer,
            UnKnow,
            LeaveFail,
            Fail
        }


        private static readonly IDictionary<string, string> ApiStatus =
        new Dictionary<string, string>
        {
                    {"Fail","失敗"},
                    { "OK", "成功" },
                    { "InternalServerError", "遠端API錯誤" },
                    { "Unauthorized", "驗證失敗" },
                    { "NotFound", "找不到符合方法" },
                    { "UnKnow", "未知錯誤" },
                    { "Customer", "成功" }

        };

        public static string GetApiStatuString(ApiStatusEnum apiStatus)
        {
            return ApiStatus[apiStatus.ToString()];
        }


    }
}

