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
    public class PlayerController : Controller
    {
        private string baseRoute = "api/player/";
        private WebApiService apiService = new WebApiService();

        // GET: Player
        public async Task<ActionResult> Index()
        {
            List<Player> players = await apiService.GetAsync<List<Player>>(baseRoute);
            return View(players);
        }

        // GET: Player/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute+id);
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View(new Player());
        }

        // POST: Player/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name")] Player player)
        {
            try
            {
                await apiService.PostAsync<Player>(baseRoute + player.Id, player);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute + id);
            return View(player);
        }

        // POST: Player/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Player player)
        {
            try
            {
                await apiService.PutAsync<Player>(baseRoute + player.Id, player);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute + id);
            return View(player);
        }

        // POST: Player/Delete/5
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
