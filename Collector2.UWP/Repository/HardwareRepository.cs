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
    public class HardwareRepository : IRepository<Hardware>
    {
        private string Root = "https://collectorv2.azurewebsites.net/api/";

        ObservableCollection<Hardware> Hardwares;

        public HardwareRepository()
        {
            Hardwares = new ObservableCollection<Hardware>();
        }

        public Task AddAsync(Hardware entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(Hardware entity)
        {
            throw new NotImplementedException();
        }

        public Hardware FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<Hardware>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Root);
                var json = await client.GetStringAsync("Hardwares");
                Hardwares = JsonConvert.DeserializeObject<ObservableCollection<Hardware>>(json);
            }

            return Hardwares;
        }

        public void UpdateAsync(Hardware entity)
        {
            throw new NotImplementedException();
        }

        Task IRepository<Hardware>.DeleteAsync(Hardware entity)
        {
            throw new NotImplementedException();
        }

        Task IRepository<Hardware>.UpdateAsync(Hardware entity)
        {
            throw new NotImplementedException();
        }
    }
}
