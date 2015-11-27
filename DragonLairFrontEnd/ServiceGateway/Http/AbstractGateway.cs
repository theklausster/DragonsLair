using System;
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

    }
}