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
    public class GameController : Controller
    {
        private string baseRoute = "api/game/";
        private WebApiService apiService = new WebApiService();
        // GET: game
        public async Task<ActionResult> Index()
        {
            List<Game> game = await apiService.GetAsync<List<Game>>(baseRoute);
            return View(game);
        }

        // GET: game/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Game game = await apiService.GetAsync<Game>(baseRoute + id);
            return View(game);
        }

        // GET: game/Create
        public ActionResult Create()
        {
            return View(new Game());
        }

        // POST: game/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name")] Game game)
        {
            try
            {
                await apiService.PostAsync<Game>(baseRoute + game.Id, game);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: game/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Game game = await apiService.GetAsync<Game>(baseRoute + id);
            return View(game);
        }

        // POST: game/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Game game)
        {
            try
            {
                await apiService.PutAsync<Game>(baseRoute + game.Id, game);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: game/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Game game = await apiService.GetAsync<Game>(baseRoute + id);
            return View(game);
        }

        // POST: game/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<Game>(baseRoute + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
