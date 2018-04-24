using Collector2.Models;
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

        public static Owner Owner
        {
            get { return new Owner() { FirstName = "Tommy", LastName = "Stenberg" }; }
        }
    }
}
