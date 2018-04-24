using Collector2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Collector2.UWP.Helpers
{
    public static class GenericDbAccess
    {
        private const string Root = "https://collectorv2.azurewebsites.net/api/";

        public static async Task GetAllObjectsAsync<T>(ObservableCollection<T> collection, string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Root);
                var json = await client.GetStringAsync(path);
                T[] objects = JsonConvert.DeserializeObject<T[]>(json);
                foreach (var o in objects)
                {
                    collection.Add(o);
                }
            }
        }

        // objectToPost => the object you want to post to the database
        // partialUri => the last part of the uri (without the Root)

        public static async Task<System.Net.HttpStatusCode> PostObjectAsync<T>(T objectToPost, string partialUri)
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(objectToPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var requestUri = Root + partialUri;
                var response = await client.PostAsync(requestUri, content);

                return response.StatusCode;
            }
        }

        public static async Task<System.Net.HttpStatusCode> AttachOrDetachImageToItemAsync (int imgId, int itemId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var requestUri = Root + $"ItemImages?imgId={imgId}&itemId={itemId}";
                var response = await client.PostAsync(requestUri, null);

                return response.StatusCode;
            }
        }

        internal static Task GetAllObjectsAsync(object requirements, string v)
        {
            throw new NotImplementedException();
        }
    }
}
