using Collector2.UWP.Services;
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
        public const string UnattachedImagesPageKey = "UnattachedImagesPage";
        public const string NewItemPageKey = "NewItemPage";
        
        public ViewModelLocator()
        {
            if(_isInitialized)
            {
                return;
            }
            _isInitialized = true;

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new FrameNavigationService();

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
            SimpleIoc.Default.Register<ApplicationViewModel>();
            SimpleIoc.Default.Register<SoftwarePageViewModel>();
            SimpleIoc.Default.Register<UnattachedImagesViewModel>();
            SimpleIoc.Default.Register<NewItemPageViewModel>();
            SimpleIoc.Default.Register<AttachToItemViewModel>();
            SimpleIoc.Default.Register<ItemCreationSelectViewModel>();
        }

        public ApplicationViewModel Application
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ApplicationViewModel>();
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

        public ApplicationViewModel ApplicationViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ApplicationViewModel>();
            }
        }

        public AttachToItemViewModel AttachToItemViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AttachToItemViewModel>();
            }
        }

        public ItemCreationSelectViewModel ItemCreationSelectViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ItemCreationSelectViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
