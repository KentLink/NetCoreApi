using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDES.Api.Model.Generic
{
    public class PaggingContainer<T>
    {
        public int Total { get; set; }
        public T Data { get; set; }
        public int TotalPage { get; set; }
    }
}