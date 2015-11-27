using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace ServiceGateway.Http
{
    public class WebApiService : AbstractGateway
    {

        public async Task<T> GetAsync<T>(string action)
        {
            return await ManageData<T>(action);
        }

        public async Task<T> PostAsync<T>(string action, T data)
        {
            return await ManageData<T>(action, data, "PostAsync");
        }

        public async Task<T> PutAsync<T>(string action, T data)
        {
            return await ManageData<T>(action, data, "PutAsync");
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string action)
        {
            HttpResponseMessage response = await Client().DeleteAsync(action);
            return response.EnsureSuccessStatusCode();
        }



    
}

    
}