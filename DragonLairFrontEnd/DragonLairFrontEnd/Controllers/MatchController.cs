using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using ServiceGateway.Http;

namespace DragonLairFrontEnd.Controllers
{
    public class MatchController : Controller
    {
       private WebApiService apiService = new WebApiService();
        private string baseRoute = "api/match/";
        // GET: Match
        public async Task<ActionResult> Index()
        {
            List<Match> matches = await apiService.GetAsync<List<Match>>(baseRoute);
            return View(matches);
        }

        // GET: Match/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Match match = await apiService.GetAsync<Match>(baseRoute + id);
            return View(match);
        }

      
        // GET: Match/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Match match = await apiService.GetAsync<Match>(baseRoute + id);
            return View(match);
        }

        // POST: Match/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Round ")] Match matchV, string teamId)
        {
            Match match = await apiService.GetAsync<Match>(baseRoute + matchV.Id);
            match.Winner = await apiService.GetAsync<Team>("api/team/" + teamId);
            try
            {
                await apiService.PutAsync(baseRoute + match.Id, match);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
    }
}
