using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public class RefreshHelper
    {
        public static RefreshHelper Instance { get; } = new RefreshHelper();

        private bool _needRefresh = false;

        public bool NeedRefresh
        {
            get { return _needRefresh; }
            set { _needRefresh = value; }
        }
    }
}
