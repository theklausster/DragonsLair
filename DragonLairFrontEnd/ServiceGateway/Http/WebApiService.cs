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

        //    using (var client = new BearerHttpClient())
            //    {
            //        if (!bearerToken.IsNullOrWhiteSpace())
            //        {
            //            //Add the authorization header
            //            client.DefaultRequestHeaders.Authorization =
            //                AuthenticationHeaderValue.Parse("Bearer " + bearerToken);
            //        }

            //        var ApiException = await client.GetAsync(BuildActionUri(action));

            //        string json = await ApiException.Content.ReadAsStringAsync();
            //        if (ApiException.IsSuccessStatusCode)
            //        {
            //            return JsonConvert.DeserializeObject<T>(json);
            //        }

            //        throw new ApiException(ApiException.StatusCode, json);
            //    }

        }
}