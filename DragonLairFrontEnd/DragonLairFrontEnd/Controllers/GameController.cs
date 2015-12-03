using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DragonLairFrontEnd.Models;
using Entities;
using ServiceGateway.Http;

namespace DragonLairFrontEnd.Controllers
{
    public class GameController : Controller
    {
        private string baseRoute = "api/game/";
        private WebApiService apiService = new WebApiService();
        // GET: game

        private async Task<GameGenreViewModel> SetUpGameGenreViewModel()
        {
            GameGenreViewModel gameGenreViewModel = new GameGenreViewModel();
            gameGenreViewModel.Genres = await apiService.GetAsync<List<Genre>>("api/genre/");
            return gameGenreViewModel;
        }
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
        public async Task<ActionResult> Create()
        {
            GameGenreViewModel gameGenreViewModel = await SetUpGameGenreViewModel();
            return View(gameGenreViewModel);
        }

        // POST: game/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, Genre")] Game game, string genreId)
        {

                
                Genre genre = await apiService.GetAsync<Genre>("api/genre/" + genreId);
                game.Genre = genre;
                if (ModelState.IsValid)
                {
                    await apiService.PostAsync<Game>(baseRoute, game);

                    return RedirectToAction("Index");
                }
            
            else
            {
                return View(await SetUpGameGenreViewModel());
            }
        }

        // GET: game/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            GameGenreViewModel gameGenreViewModel = await SetUpGameGenreViewModel();
            gameGenreViewModel.Game = await apiService.GetAsync<Game>(baseRoute + id);
            gameGenreViewModel.Game.Genre =
                await apiService.GetAsync<Genre>("api/genre/" + gameGenreViewModel.Game.Genre.Id);
            gameGenreViewModel.Genres = await apiService.GetAsync<List<Genre>>("api/genre/");
            return View(gameGenreViewModel);
        }

        // POST: game/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Game game, string genreId)
        {
            try
            {
                Genre genre = await apiService.GetAsync<Genre>("api/genre/" + genreId);
                
                game.Genre = genre;
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
