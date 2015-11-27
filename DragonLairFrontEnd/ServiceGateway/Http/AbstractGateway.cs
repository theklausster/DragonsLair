using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ServiceGateway.Http
{
    public abstract class AbstractGateway
    {
        public HttpClient Client()
        {
            HttpClient client = new HttpClient();
            string baseAddress = "http://dragonapi.devjakobsen.dk/";
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public async Task<T> ManageData<T>(string action)
        {
            var response = await Client().GetAsync(action);
            string json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            throw new ApiException(response.StatusCode, json);
        }

        public async Task<T> ManageData<T>(string action, T t, string type)
        {
            HttpResponseMessage response = null;
            switch(type)
            {
                case "Post":
                 response = await Client().PostAsJsonAsync(action, t);
                break;
                case "Put":
                 response = await Client().PutAsJsonAsync(action, t);
                break;
            }

            string json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {             
                return JsonConvert.DeserializeObject<T>(json);
            }
            
            throw new ApiException(response.StatusCode, json);
        }

    }
}