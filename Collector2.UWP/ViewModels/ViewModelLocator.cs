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
        public const string UnattachedImageEditPage = "UnattachedImageEditPage";
        public const string SoftwareDetailsPage = "SoftwareDetailsPage";
        
        public ViewModelLocator()
        {
            if(_isInitialized)
            {

            }
            _isInitialized = true;

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new FrameNavigationService();

            nav.Configure(SoftwarePageKey, typeof(SoftwarePage));
            nav.Configure(UnattachedImagesPageKey, typeof(UnattachedImagesPage));
            nav.Configure(UnattachedImageEditPage, typeof(UnattachedImageEditPage));
            nav.Configure(SoftwareDetailsPage, typeof(SoftwareDetailsPage));
            
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
            SimpleIoc.Default.Register<UnattachedImageEditViewModel>();
            SimpleIoc.Default.Register<AttachToItemViewModel>();
            SimpleIoc.Default.Register<NewSoftwareViewModel>();
            SimpleIoc.Default.Register<CreateItemViewModel>();
            SimpleIoc.Default.Register<SoftwareDetailsViewModel>();
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

        public UnattachedImageEditViewModel UnattachedImageEditViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UnattachedImageEditViewModel>();
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

        public NewSoftwareViewModel NewSoftwareViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewSoftwareViewModel>();
            }
        }

        public CreateItemViewModel CreateItemViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateItemViewModel>();
            }
        }

        public SoftwareDetailsViewModel SoftwareDetailsViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SoftwareDetailsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
