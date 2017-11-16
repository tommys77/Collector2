using Collector2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public class ItemSelectionHelper : INotifyPropertyChanged
    {
        private static ItemImage CurrentItemImage { get; set; }
        private static Software currentSoftware;
        private static Hardware currentHardware;
        
        public static void SetCurrentItemImage(ItemImage itemImage)
        {
            CurrentItemImage = itemImage;
        }

        public static ItemImage GetCurrentItemImage()
        {
            return CurrentItemImage;
        }

        public static Software CurrentSoftware
        {
            get { return currentSoftware; }
            set
            {
                if (!Equals(CurrentSoftware, value))
                {
                    currentSoftware = value;
                }
            }
        }

        public static Hardware CurrentHardware
        {
            get { return currentHardware; }
            set
            {
                if (!Equals(CurrentHardware, value))
                {
                    currentHardware = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
