using Collector2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public static class ItemSelectionHelper
    {
        private static ItemImage currentItemImage;
        private static Software currentSoftware;
        private static Hardware currentHardware;

        public static ItemImage CurrentItemImage
        {
            get { return currentItemImage; }
            set
            {
                if (!Equals(CurrentItemImage, value))
                {
                    currentItemImage = value;
                }
            }
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


    }
}
