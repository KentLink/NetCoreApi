using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDES.Api.Models
{
    public class ApiResult<DataT>
    {
        public Enum.ApiStatusEnum Status { get; set; }
        public string Message { get; set; }
        public DataT Data { get; set; }
    }
}