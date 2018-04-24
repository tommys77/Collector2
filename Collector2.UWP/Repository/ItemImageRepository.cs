using Collector2.Models;
using Collector2.UWP.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Repository
{
    public class ItemImageRepository : IRepository<ItemImage>
    {

        ObservableCollection<ItemImage> Images;

        private System.Net.HttpStatusCode _statusCode;

        private const string Root = "https://collectorv2.azurewebsites.net/api/";

        public Task AddAsync(ItemImage entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(ItemImage entity)
        {
            throw new NotImplementedException();
        }

        public ItemImage FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<ItemImage>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Root);
                var json = await client.GetStringAsync("UnattachedImages");
                Images = JsonConvert.DeserializeObject<ObservableCollection<ItemImage>>(json);
            }
            return Images;
        }

        public async Task AttachOrDetachImageToItem(int imgId, int itemId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var requestUri = Root + $"AttachOrDetachImage?imgId={imgId}&itemId={itemId}";
                var response = await client.PostAsync(requestUri, null);
                _statusCode = response.StatusCode;

            }
        }

        public void UpdateAsync(ItemImage entity)
        {
            throw new NotImplementedException();
        }

        Task IRepository<ItemImage>.DeleteAsync(ItemImage entity)
        {
            throw new NotImplementedException();
        }

        Task IRepository<ItemImage>.UpdateAsync(ItemImage entity)
        {
            throw new NotImplementedException();
        }

        public System.Net.HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }
    }
}
