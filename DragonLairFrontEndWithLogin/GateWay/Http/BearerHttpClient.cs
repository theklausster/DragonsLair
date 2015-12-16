using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Security;
using GateWay.Models;
using Newtonsoft.Json;

namespace GateWay.Http
{
    public class BearerHttpClient : HttpClient
    {
        public BearerHttpClient()
        {
            SetToken();
        }

        private void SetToken()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null) return;

            var model = JsonConvert.DeserializeObject<UserModel>(authTicket.UserData);

            if (model != null)
            {
                //Add the authorization header
                DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + model.Token);
            }
        }
    }
}