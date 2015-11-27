using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace ServiceGateway.Http
{
    public class WebApiService : AbstractGateway
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod(StackTrace stack)
        {           
            StackFrame sf = stack.GetFrame(0);
            return sf.GetMethod().Name;
        }
        public async Task<T> GetAsync<T>(string action)
        {
            return await ManageData<T>(action);
        }

        public async Task<T> PostAsync<T>(string action, T data)
        {
            StackTrace stack = new StackTrace();
            return await ManageData<T>(action, data, GetCurrentMethod(stack));
        }

        public async Task<T> PutAsync<T>(string action, T data)
        {
            StackTrace stack = new StackTrace();
            return await ManageData<T>(action, data, GetCurrentMethod(stack));
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string action)
        {
            HttpResponseMessage response = await Client().DeleteAsync(action);
            return response.EnsureSuccessStatusCode();
        }



    
}

    
}