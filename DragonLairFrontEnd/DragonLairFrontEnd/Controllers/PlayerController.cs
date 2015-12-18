using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Lifetime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DragonLairFrontEnd.Models;
using Entities;
using Microsoft.Owin.Security.Provider;
using ServiceGateway.Http;

namespace DragonLairFrontEnd.Controllers
{
    public class PlayerController : Controller
    {
        private string baseRoute = "api/player/";
        private WebApiService apiService = new WebApiService();

        //GET: Player
        public async Task<ActionResult> Index()
        {
            List<Player> players = await apiService.GetAsync<List<Player>>(baseRoute);
            return View("Index",players);
        }

        // GET: Player/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute + id);
            return View("Details", player);
        }

        // GET: Player/Create
      public ActionResult Create()
        {
            return View("Create", new Player());
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name")] Player player)
        {
            try
            {
                await apiService.PostAsync<Player>(baseRoute + player.Id, player);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Player/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            PlayerModel playerModel = new PlayerModel();
            playerModel.Player = await apiService.GetAsync<Player>(baseRoute + id);
            playerModel.Teams = await apiService.GetAsync<List<Team>>("api/team/");
            playerModel.SetupList(playerModel.Player, playerModel.Teams);
            return View("Edit", playerModel);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Player player, string[] teamId)
        {
           
            if (teamId == null) teamId = new string[] {};
            {
                player.Teams = new List<Team>();
                foreach (var id in teamId)
                {
                    Team team = await apiService.GetAsync<Team>("api/team/" + id);
                    player.Teams.Add(team);
                }
            }

            try
            {
                await apiService.PutAsync<Player>(baseRoute + player.Id, player);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Player/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute + id);
            return View("Delete", player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<Player>(baseRoute + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }

        public async Task<ActionResult> Remove(int teamId, int playerId)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute + playerId);
            Team team = player.Teams.FirstOrDefault(a => a.Id == teamId);
            player.Teams.Remove(team);
            await apiService.PutAsync<Player>(baseRoute + player.Id, player);
            player = await apiService.GetAsync<Player>(baseRoute + playerId);
            return RedirectToAction("Edit/" + player.Id);

        }

        public async Task<ActionResult> Add(int teamId, int playerId)
        {
            Player player = await apiService.GetAsync<Player>(baseRoute + playerId);
            Team team = await apiService.GetAsync<Team>("api/team/" + teamId);
            player.Teams.Add(team);
            string route = baseRoute + player.Id;
            await apiService.PutAsync<Player>(route, player);
            player = await apiService.GetAsync<Player>(baseRoute + playerId);
            return RedirectToAction("Edit/" + player.Id);
        }

       
    }
}
