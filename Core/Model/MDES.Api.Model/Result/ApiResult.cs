using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDES.Api.Model.Result
{
    public class ApiResult<DataT>
    {
        /// <summary>
        /// 後端通訊回覆用的
        /// </summary>
        public Variable.Enum.ApiStatusEnum ApiStatus { get; set; }

        /// <summary>
        /// 封包用的
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 封包資料
        /// </summary>
        public DataT Data { get; set; }
    }
}