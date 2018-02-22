using MDES.Api.Model.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDES.Api.Dao
{
    public static class URLVariable
    {
        #region ApiRootUrl
        public static string ApiRootUrl
        {
            get { return MDESUrl + "Api/"; }
        }

        /// <summary>
        /// ApiURL
        /// </summary>
        public static string MDESUrl
        {
            get { return ConfigProvider.MDESUrl; }
        }
        #endregion

        #region API
        public static string TestData
        {
            get { return "MemberInfo/Case"; }
        }

        #endregion

    }
}
