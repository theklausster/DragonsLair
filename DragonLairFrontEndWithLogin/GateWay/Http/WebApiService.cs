﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace GateWay.Http
{
    public class WebApiService
    {
        private WebApiService(string baseUri)
        {
            BaseUri = baseUri;
        }

        private static WebApiService _instance;

        public static WebApiService Instance
        {
            get { return _instance ?? (_instance = new WebApiService(ConfigurationManager.AppSettings["WebApiUri"])); }
        }

        public string BaseUri { get; private set; }

        public async Task<T> AuthenticateAsync<T>(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(BuildActionUri("/Token"), new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("userName", userName),
                    new KeyValuePair<string, string>("password", password)
                }));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task<T> GetAsync<T>(string action, string bearerToken = null)
        {
            using (var client = new BearerHttpClient())
            {
                if (bearerToken != null)
                {
                    //Add the authorization header
                    client.DefaultRequestHeaders.Authorization =
                        AuthenticationHeaderValue.Parse("Bearer " + bearerToken);
                }

                var result = await client.GetAsync(BuildActionUri(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task PutAsync<T>(string action, T data)
        {
            using (var client = new BearerHttpClient())
            {
                var result = await client.PutAsJsonAsync(BuildActionUri(action), data);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }
        public async Task PostAsync<T>(string action, T data)
        {
            using (var client = new BearerHttpClient())
            {
                var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new ApiException(result.StatusCode, json);
            }
        }

        private string BuildActionUri(string action)
        {
            return BaseUri + action;
        }
    }
}