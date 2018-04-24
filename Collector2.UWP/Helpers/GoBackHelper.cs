using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public class GoBackHelper : INotifyPropertyChanged
    {
        public static GoBackHelper Instance { get; } = new GoBackHelper();
        private static bool _canGoBack = false;

        public bool CanGoBack
        {
            get { return _canGoBack; }
            set
            {
                _canGoBack = value;
                NotifyPropertyChanged(nameof(CanGoBack));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
