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
        public const string UnattachedImagesPageKey = "UndefinedItemsPage";
        public const string NewItemPageKey = "NewItemPage";
        
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
            nav.Configure(UnattachedImagesPageKey, typeof(UnattachedImagesPage));
            nav.Configure(NewItemPageKey, typeof(NewItemPage));
            
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
            SimpleIoc.Default.Register<UnattachedImagesViewModel>();
            SimpleIoc.Default.Register<NewItemPageViewModel>();
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

        public UnattachedImagesViewModel UnattachedImagesViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UnattachedImagesViewModel>();
            }
        }

        public NewItemPageViewModel NewItemViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewItemPageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
