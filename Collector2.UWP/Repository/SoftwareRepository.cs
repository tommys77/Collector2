using Collector2.Models;
using Collector2.Models.ViewModels;
using Collector2.UWP.Helpers;
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
    public class SoftwareRepository : IRepository<Software>
    {
        ObservableCollection<SoftwareViewModel> SoftwareViewModels;

        private System.Net.HttpStatusCode _statusCode;

        private const string Root = "https://collectorv2.azurewebsites.net/api/";

        public SoftwareRepository()
        {
        }

        public async Task<ObservableCollection<SoftwareViewModel>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Root);
                var json = await client.GetStringAsync("Softwares");
                SoftwareViewModels = JsonConvert.DeserializeObject<ObservableCollection<SoftwareViewModel>>(json);
            }

            return SoftwareViewModels;
        }

        public async Task AddAsync(Software software)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(software);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var requestUri = Root + "Softwares";

                var response = await client.PostAsync(requestUri, content);
                _statusCode = response.StatusCode;
            }
        }

        public async Task AddAsync(Software software, int imgId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(software);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var requestUri = Root + "Softwares?imgId=" + imgId;

                var response = await client.PostAsync(requestUri, content);
                _statusCode = response.StatusCode;
            }
        }

        public Task DeleteAsync(Software entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Software entity)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(entity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var requestUri = Root + "UpdateSoftware";

                var response = await client.PostAsync(requestUri, content);
                _statusCode = response.StatusCode;
            }
        }

        public Software FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ObservableCollection<Software>> IRepository<Software>.GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public System.Net.HttpStatusCode StatusCode
        {
            get
            {
                return _statusCode;
            }
        }
    }
}
