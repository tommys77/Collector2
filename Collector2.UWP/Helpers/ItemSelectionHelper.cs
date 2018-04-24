using AutoMapper;
using Collector2.Models;
using Collector2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public class ItemSelectionHelper
    {
        private static ItemImage _currentItemImage;
        private static SoftwareViewModel _currentSoftware;
        private static Hardware currentHardware;

        public static void SetCurrentItemImage(ItemImage itemImage)
        {
            _currentItemImage = itemImage;
        }

        public static ItemImage GetCurrentItemImage()
        {
            return _currentItemImage;
        }


        public static void SetCurrentSoftware(SoftwareViewModel software)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SoftwareViewModel, SoftwareViewModel>();
            });

            var mapper = config.CreateMapper();

            _currentSoftware = mapper.Map<SoftwareViewModel, SoftwareViewModel>(software);
            
        }

        public static SoftwareViewModel GetCurrentSoftware()
        {
            return _currentSoftware;
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
