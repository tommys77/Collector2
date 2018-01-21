using Collector2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public static class SoftwareHardwareWrapper
    {
        public static ObservableCollection<Software> Softwares { get; set; }
        public static ObservableCollection<Hardware> Hardwares { get; set; }
    }
}
