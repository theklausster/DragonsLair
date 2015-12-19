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
            TournamentViewModel.SortTeams(TournamentViewModel.Tournament.Groups);
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, StartDate")] Tournament tournament)
        {
            try
            {
                await TournamentViewModel.CreateTournament(tournament);
                return RedirectToAction("Index");
            }
            catch
            {
                string error = "Something went wrong. Try to create a 2vs2 with even players";
                TournamentViewModel.Error = error;
                return RedirectToAction("Create");
            }
        }

    }
}