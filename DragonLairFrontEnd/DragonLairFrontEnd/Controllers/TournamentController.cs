using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Entities;
using ServiceGateway.Http;
using DragonLairFrontEnd.Models;

namespace DragonLairFrontEnd.Controllers
{
    public class TournamentController : Controller
    {
        private TournamentViewModel TournamentViewModel;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (Session["TournamentViewModel"] == null) Session["TournamentViewModel"] = new TournamentViewModel();
            TournamentViewModel = (TournamentViewModel)Session["TournamentViewModel"];
        }

        // GET: tournament
        public async Task<ActionResult> Index()
        {
            ResetSession();
            await TournamentViewModel.PopulateIndexData();
            return View(TournamentViewModel.Tournaments);
        }

        // GET: tournament/Details/5
        public async Task<ActionResult> Details(int id)
        {
            await TournamentViewModel.PopulateDetailsData(id);
            return View(TournamentViewModel);
        }

        // GET: tournament/Create
        public async Task<ActionResult> Create()
        {
            await TournamentViewModel.PopulateCreateData();
            return View(TournamentViewModel);
        }


        public ActionResult AddPlayer(int playerId)
        {
            TournamentViewModel.AddPlayer(playerId);
            return RedirectToAction("Create");
        }

        public ActionResult RemovePlayer(int playerId)
        {
            TournamentViewModel.RemovePlayer(playerId);
            return RedirectToAction("Create");
        }

        public ActionResult AddTournamentType(int tournamentTypeId)
        {
            TournamentViewModel.AddTourneyType(tournamentTypeId);
            return RedirectToAction("Create");
        }

        public ActionResult RemoveTournamentType()
        {
            TournamentViewModel.RemoveTourneyType();
            return RedirectToAction("Create");
        }

        public ActionResult AddGame(int gameId)
        {
            TournamentViewModel.AddGame(gameId);
            return RedirectToAction("Create");
        }
        public ActionResult RemoveGame()
        {
            TournamentViewModel.RemoveGame();
            return RedirectToAction("Create");
        }

        public void ResetSession()
        {
            Session.Clear();
        }


        //POST: tournament/Create
       [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, StartDate")] Tournament tournament)
        {
            try
            {
                await TournamentViewModel.CreateTournament(tournament);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //    // GET: tournament/Edit/5
        //    public async Task<ActionResult> Edit(int id)
        //    {
        //        Tournament tournament = await apiService.GetAsync<Tournament>(baseRoute + id);
        //        return View(tournament);
        //    }

        //    // POST: tournament/Edit/5
        //    [HttpPost]
        //    public async Task<ActionResult> Edit([Bind(Include = "Id, Name, StartDate")] Tournament tournament)
        //    {
        //        try
        //        {
        //            await apiService.PutAsync<Tournament>(baseRoute + tournament.Id, tournament);

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: tournament/Delete/5
        //    public async Task<ActionResult> Delete(int id)
        //    {
        //        Tournament tournament = await apiService.GetAsync<Tournament>(baseRoute + id);
        //        return View(tournament);
        //    }

        //    // POST: tournament/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    public async Task<ActionResult> DeleteConfirmed(int id)
        //    {
        //        try
        //        {
        //            await apiService.DeleteAsync<Player>(baseRoute + id);

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}