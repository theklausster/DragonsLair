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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
