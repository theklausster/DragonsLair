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
    public class TournamentTypeController : Controller
    {
        private string baseRoute = "api/tournamentType/";
        private WebApiService apiService = new WebApiService();
        // GET: TournamentType
        public async Task<ActionResult> Index()
        {
            List<TournamentType> tournamentTypes = await apiService.GetAsync<List<TournamentType>>(baseRoute);
            return View(tournamentTypes);
        }

        // GET: TournamentType/Details/5
        public async Task<ActionResult> Details(int id)
        {
            TournamentType tournamentType = await apiService.GetAsync<TournamentType>(baseRoute + id);
            return View(tournamentType);
        }

        // GET: TournamentType/Create
        public ActionResult Create()
        {
            return View(new TournamentType());
        }

        // POST: TournamentType/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Type")] TournamentType tournamentType)
        {
            try
            {
                await apiService.PostAsync<TournamentType>(baseRoute + tournamentType.Id, tournamentType);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentType/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            TournamentType tournamentType = await apiService.GetAsync<TournamentType>(baseRoute + id);
            return View(tournamentType);
        }

        // POST: TournamentType/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Type")] TournamentType tournamentType)
        {
            try
            {
                await apiService.PutAsync<TournamentType>(baseRoute + tournamentType.Id, tournamentType);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentType/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            TournamentType tournamentType = await apiService.GetAsync<TournamentType>(baseRoute + id);
            return View(tournamentType);
        }

        // POST: TournamentType/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<TournamentType>(baseRoute + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
