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
    public class TournamentController : Controller
    {
        private string baseRoute = "api/tournament/";
        private WebApiService apiService = new WebApiService();

        // GET: tournament
        public async Task<ActionResult> Index()
        {
            List<Tournament> tournament = await apiService.GetAsync<List<Tournament>>(baseRoute);
            return View(tournament);
        }

        // GET: tournament/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Tournament tournament = await apiService.GetAsync<Tournament>(baseRoute + id);
            return View(tournament);
        }

        // GET: tournament/Create
        public ActionResult Create()
        {
            return View(new Tournament());
        }

        // POST: tournament/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, StartDate")] Tournament tournament)
        {
            try
            {
                await apiService.PostAsync<Tournament>(baseRoute + tournament.Id, tournament);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tournament/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Tournament tournament = await apiService.GetAsync<Tournament>(baseRoute + id);
            return View(tournament);
        }

        // POST: tournament/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name, StartDate")] Tournament tournament)
        {
            try
            {
                await apiService.PutAsync<Tournament>(baseRoute + tournament.Id, tournament);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tournament/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Tournament tournament = await apiService.GetAsync<Tournament>(baseRoute + id);
            return View(tournament);
        }

        // POST: tournament/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<Player>(baseRoute + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
