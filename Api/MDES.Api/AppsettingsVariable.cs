using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDES.Api
{
    public class AppsettingsVariable
    {
        public AppsettingsVariable()
        {

        }
  

        public string GetAppsettingsVariableString(IConfiguration config, string key)
        {
            return config[key];
        }


        public string GetDefaultDB(IConfiguration config)
        {
            return config["Default"];
        }

    }
}
