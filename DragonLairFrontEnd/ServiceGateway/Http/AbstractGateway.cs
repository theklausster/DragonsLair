using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace ServiceGateway.Http
{
    public abstract class AbstractGateway
    {
        public HttpClient Client()
        {
            HttpClient client = new HttpClient();
            string baseAddress = "http://localhost:41257/";
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

                dynamic entity = JsonConvert.DeserializeObject<T>(json);
                return entity;
            }
            throw new ApiException(response.StatusCode, json);

            }

        public async Task<T> ManageData<T>(string action, T t, string type)
        {
            HttpResponseMessage response = null;
            switch(type)
            {
                case "PostAsync":
                 response = await Client().PostAsJsonAsync(action, t);
                break;
                case "PutAsync":
                 response = await Client().PutAsJsonAsync(action, t);
                break;
            }

            string json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic entity = JsonConvert.DeserializeObject<T>(json);
                return entity;
            }
            
            throw new ApiException(response.StatusCode, json);
        }



    }
}