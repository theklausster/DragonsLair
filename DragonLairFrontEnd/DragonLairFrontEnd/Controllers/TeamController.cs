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
 
        private TeamViewModel TeamViewModel;
        private WebApiService ApiService = new WebApiService();
        private string BaseRoute = "api/Team/";
       


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["TeamViewModel"] == null)
            {
                Session["TeamViewModel"] = new TeamViewModel();
            }
            TeamViewModel = (TeamViewModel)Session["TeamViewModel"];

        }

        // GET: Team
        public async Task<ActionResult> Index()
        {
            ResetSessions();
            await TeamViewModel.PopulateData();
            return View(TeamViewModel.Teams);
        }

        // GET: Team/Details/5
        public async Task<ActionResult> Details(int id)
        {
            await TeamViewModel.PopulateData(id);
            return View(TeamViewModel.Team);
        }

        // GET: Team/Create
        public async Task<ActionResult> Create()
        {
            TeamViewModel.ActionName = "Create";
            await TeamViewModel.PopulateData();
            return View(TeamViewModel);
        }

        // POST: Team/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name, Win, Loss, Draw")] Team team)
        {
            try
            {
                team.Players = TeamViewModel.SelectedPlayers;
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
        public async Task<ActionResult> Edit(int id)
        {
            TeamViewModel.ActionName = "Edit";
            await TeamViewModel.PopulateData(id);
            return View(TeamViewModel);
        }

        public ActionResult Add(int playerId)
        {
            TeamViewModel.Add(playerId);
            if(TeamViewModel.ActionName.Equals("Edit")) return RedirectToAction("Edit", "Team", new { id = TeamViewModel.Team.Id });
            if(TeamViewModel.ActionName.Equals("Create")) return RedirectToAction("Create", "Team");
            return null;
        }

        public ActionResult Remove(int playerId)
        {
            TeamViewModel.Remove(playerId);
            if (TeamViewModel.ActionName.Equals("Edit")) return RedirectToAction("Edit", "Team", new { id = TeamViewModel.Team.Id });
            if (TeamViewModel.ActionName.Equals("Create")) return RedirectToAction("Create", "Team");
            return null;
        }


        // POST: Team/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name, Win, Loss, Draw")] Team team)
        {
            try
            {
                team.Players = TeamViewModel.SelectedPlayers;
                await  ApiService.PutAsync<Team>(BaseRoute + team.Id, team);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", team.Id);
            }
        }

        // GET: Team/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await TeamViewModel.PopulateData(id);
            return View(TeamViewModel.Team);
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
