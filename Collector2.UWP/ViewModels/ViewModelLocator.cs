using Collector2.UWP.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.ViewModels
{
    public class ViewModelLocator
    {
        private static bool _isInitialized;
        public const string MainPageKey = "MainPage";
        public const string SoftwarePageKey = "SoftwarePage";
        public const string UndefinedItemsPageKey = "UndefinedItemsPage";
        
        public ViewModelLocator()
        {
            if(_isInitialized)
            {
                return;
            }
            _isInitialized = true;

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();

            nav.Configure(MainPageKey, typeof(MainPage));
            nav.Configure(SoftwarePageKey, typeof(SoftwarePage));
            nav.Configure(UndefinedItemsPageKey, typeof(UndefinedItemsPage));
            
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }
            
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SoftwarePageViewModel>();
            SimpleIoc.Default.Register<UndefinedItemsViewModel>();
            
        }

        public MainPageViewModel MainPageViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public SoftwarePageViewModel SoftwarePageViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SoftwarePageViewModel>();
            }
        }

        public UndefinedItemsViewModel UndefinedItemsViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UndefinedItemsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
