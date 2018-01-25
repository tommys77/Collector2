using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Config
{
    public static class CollectorConfig
    {
        private const string _apiRoot = "https://collectorv2.azurewebsites.net/api/";

        public static string ApiRoot
        {
            get
            {
                return _apiRoot;
            }
        }
    }
}
