using DragonLairFrontEnd.Models;
using Entities;
using ServiceGateway.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DragonLairFrontEnd.Controllers
{
    public class TeamController : Controller
    {

        private WebApiService ApiService = new WebApiService();
        private string BaseRoute = "api/Team/";
       


        protected override void Initialize(RequestContext requestContext)
        {
           base.Initialize(requestContext);
        }

        // GET: Team
        public async Task<ActionResult> Index(TeamViewModel teamViewModel)
        {
            ResetSessions();
            await teamViewModel.PopulateData();
            return View(teamViewModel.Teams);
        }

        // GET: Team/Details/5
        public async Task<ActionResult> Details(int id, TeamViewModel teamViewModel)
        {
            await teamViewModel.PopulateData(id);
            return View(teamViewModel.Team);
        }

        // GET: Team/Create
        public async Task<ActionResult> Create(TeamViewModel teamViewModel)
        {
            teamViewModel.ActionName = "Create";
            await teamViewModel.PopulateData();
            return View(teamViewModel);
        }

        // POST: Team/Create
        [HttpPost]
        public async Task<ActionResult> Create(TeamViewModel teamViewModel,[Bind(Include = "Name, Win, Loss, Draw")] Team team)
        {
            try
            {
                team.Players = teamViewModel.SelectedPlayers;
                await ApiService.PostAsync<Team>(BaseRoute, team);

                return RedirectToAction("Index");
            }
            catch
            {
                ResetSessions();
                return RedirectToAction("Create");
            }
        }

        // GET: Team/Edit/5
        public async Task<ActionResult> Edit(TeamViewModel teamViewModel,int id)
        {
            teamViewModel.ActionName = "Edit";
            await teamViewModel.PopulateData(id);
            return View(teamViewModel);
        }

        public ActionResult Add(TeamViewModel teamViewModel,int playerId)
        {
            teamViewModel.Add(playerId);
            if(teamViewModel.ActionName.Equals("Edit")) return RedirectToAction("Edit", "Team", new { id = teamViewModel.Team.Id });
            if(teamViewModel.ActionName.Equals("Create")) return RedirectToAction("Create", "Team");
            return null;
        }

        public ActionResult Remove(TeamViewModel teamViewModel, int playerId)
        {
            teamViewModel.Remove(playerId);
            if (teamViewModel.ActionName.Equals("Edit")) return RedirectToAction("Edit", "Team", new { id = teamViewModel.Team.Id });
            if (teamViewModel.ActionName.Equals("Create")) return RedirectToAction("Create", "Team");
            return null;
        }


        // POST: Team/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(TeamViewModel teamViewModel,[Bind(Include = "Id, Name, Win, Loss, Draw")] Team team)
        {
            try
            {
                team.Players = teamViewModel.SelectedPlayers;
                await  ApiService.PutAsync<Team>(BaseRoute + team.Id, team);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", team.Id);
            }
        }

        // GET: Team/Delete/5
        public async Task<ActionResult> Delete(TeamViewModel teamViewModel,int id)
        {
            await teamViewModel.PopulateData(id);
            return View(teamViewModel.Team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await ApiService.DeleteAsync<Team>(BaseRoute + id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void ResetSessions()
        {
            Session.Clear();
        }
    }

}
