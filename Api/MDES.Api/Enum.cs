using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDES.Api
{
    public class Enum
    {
        public enum ApiStatusEnum
        {
            OK = 0,
            InternalServerError = 1,
            Unauthorized = 2,
            NotFound = 3,
            Customer = 4,
            UnKnow = 5,
            LeaveFail = 6,
            Fail = 7
        }
    }
}
