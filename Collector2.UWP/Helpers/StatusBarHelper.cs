using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public class StatusBarHelper : ObservableObject

    {
        private static readonly StatusBarHelper _instance = new StatusBarHelper();
        private static string _statusBarMessage;

        public static StatusBarHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        public string StatusBarMessage
        {
            get
            {
                if (_statusBarMessage == null)
                {
                    _statusBarMessage = "Welcome";
                }
                return _statusBarMessage;
            }
            set
            {
                _statusBarMessage = value;
                RaisePropertyChanged("StatusBarMessage");
            }
        }
    }
}
