using Entities;
using ServiceGateway.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DragonLairFrontEnd.Controllers
{
    public class TeamController : Controller
    {
        private string baseRoute = "api/Team/";
        private WebApiService apiService = new WebApiService();

        // GET: Team
        public async Task<ActionResult> Index()
        {
            List<Team> teams = await apiService.GetAsync<List<Team>>(baseRoute);
            return View(teams);
        }

        // GET: Team/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Team team = await apiService.GetAsync<Team>(baseRoute + id);
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View(new Team());
        }

        // POST: Team/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, Win, Loss, Draw")] Team team)
        {
            try
            {
                await apiService.PostAsync<Team>(baseRoute + team.Id, team);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Team team = await apiService.GetAsync<Team>(baseRoute + id);
            return View(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name, Win, Loss, Draw")] Team team)
        {
            try
            {
                await apiService.PutAsync<Team>(baseRoute + team.Id, team);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Team team = await apiService.GetAsync<Team>(baseRoute + id);
            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<Team>(baseRoute + id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
