using Collector2.UWP.Views;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Collector2.UWP.Services
{
    public class FrameNavigationService : INavigationService
    {
        private readonly IDictionary<string, Type> _pages = new Dictionary<string, Type>();
        private static Frame AppFrame => ((Window.Current.Content as Frame)?.Content as Shell)?.AppFrame;


        public const string RootPageKey = "-- ROOT --";
        public const string UnknownPageKey = "-- UNKNOWN --";

        public string CurrentPageKey
        {
            get
            {
                var frame = AppFrame;
                if (frame.BackStackDepth == 0)
                    return RootPageKey;
                if (frame.Content == null)
                    return UnknownPageKey;
                var type = frame.Content.GetType();

                lock (_pages)
                {
                    if (_pages.Values.All(v => v != type))
                        return UnknownPageKey;

                    var item = _pages.Single(i => i.Value == type);

                    return item.Key;
                }
            }
        }

        public void Configure(string page, Type type)
        {
            lock (_pages)
            {
                if (_pages.ContainsKey(page))
                    throw new ArgumentException("The specified key is already registered.");
                if (_pages.Values.Any(v => v == type))
                    throw new ArgumentException("The specified view has already been registered under a different name.");

                _pages.Add(page, type);
            }
        }

        public void NavigateTo(string page, object parameter)
        {
            lock (_pages)
            {
                if (!_pages.ContainsKey(page))
                    throw new ArgumentException("Unable to find a page registered with the specified name.");

                System.Diagnostics.Debug.Assert(AppFrame != null);
                AppFrame.Navigate(_pages[page], parameter);
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void GoBack()
        {
            System.Diagnostics.Debug.Assert(AppFrame != null);
            if (AppFrame.CanGoBack)
                AppFrame.GoBack();
        }
    }
}
