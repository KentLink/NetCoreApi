using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variable.Generic;

namespace MDES.Api.Model.Generic
{
    public class PaggingArg
    {
        /// <summary>
        /// 頁碼
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每頁筆數
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 欲排序欄位-名稱
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 正反序設定
        /// </summary>
        public Variable.Generic.Enum.SortExpression SortExpression { get; set; }
    }
}
