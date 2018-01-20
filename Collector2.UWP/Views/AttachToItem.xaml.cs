using Collector2.UWP.Common;
using GalaSoft.MvvmLight.Command;
using System;
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Collector2.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AttachToItem : BindablePage
    {

        private RelayCommand _getAllItemsCommand;
        public string Status;
        
        private const string BASE_URI = "http://collectorv2.azurewebsites.net/api/";

        public AttachToItem()
        {
            this.InitializeComponent();
        }

        //public RelayCommand GetAllItemsCommand
        //{
        //    get
        //    {
        //        return (_getAllItemsCommand = new RelayCommand(async () =>
        //        {
        //            try
        //            {
        //                using (var client = new HttpClient())
        //                {
        //                    client.BaseAddress = new Uri(BASE_URI);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Status = "Error: " + ex;
        //            }
        //        }));
        //    }
        //}
    }
}
