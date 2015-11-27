using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceGateway.Http
{
    public class WebApiService : AbstractGateway
    {
        public async Task<T> GetAsync<T>(string action)
        {
            var response = await Client().GetAsync(action);
            string json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            throw new ApiException(response.StatusCode, json);
        }
    }
}