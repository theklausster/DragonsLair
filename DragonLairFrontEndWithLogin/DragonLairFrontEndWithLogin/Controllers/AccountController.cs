using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using DragonLairFrontEndWithLogin.Models;
using GateWay.Http;
using Newtonsoft.Json;
using WebApplicationCleint.Filters;


namespace WebApplicationCleint.Controllers
{
    [HandleApiError]
    public class AccountController : ApiController
    {
        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            model.Role = "Member";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await WebApiService.Instance.PostAsync("/api/Account/Register", model);
                return View("Registered");
            }
            catch (ApiException ex)
            {
                //No 200 OK result, what went wrong?
                HandleBadRequest(ex);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                throw;
            }
        }

        // GET: Account/SignIn
        public ActionResult SignIn(string returnUrl)
        {
            Session["RedirectUrl"] = returnUrl;
            return View();
        }

        // POST: Account/SignIn
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                //Find if there is a redirect Url. Then remove it for next time!
                var redirectUrl = Session["RedirectUrl"] as string;
                Session["RedirectUrl"] = null;
                var result = await WebApiService.Instance.AuthenticateAsync<SignInResult>(model.Email, model.Password);
                var userInfo = await WebApiService.Instance.GetAsync<UserModel>("/api/Account/me", result.AccessToken);
                userInfo.Token = result.AccessToken;
                string json = JsonConvert.SerializeObject(userInfo);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now, DateTime.Now.AddMinutes(20), model.RememberMe, json, "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);

                return Redirect(redirectUrl ?? "/");
            }
            catch (ApiException ex)
            {
                //No 200 OK result, what went wrong?
                HandleBadRequest(ex);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                throw;
            }
        }

        private async Task<UserModel> GetUserInfo()
        {
            // We wouldn't normally be likely to do this:
            //bool role = HttpContext.User.IsInRole("Administrator");

            try
            {
                var info = await WebApiService.Instance.GetAsync<UserModel>("/api/Account/me");
                return info;
                // return View("Registered");
            }
            catch (ApiException ex)
            {
                //No 200 OK result, what went wrong?
                HandleBadRequest(ex);
                return null;
            }
        }

        [System.Web.Mvc.Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}