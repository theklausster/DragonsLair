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
    public class GenreController : Controller
    {
        private string baseRoute = "api/genre/";
        private WebApiService apiService = new WebApiService();

        // GET: genre
        public async Task<ActionResult> Index()
        {
            List<Genre> genres = await apiService.GetAsync<List<Genre>>(baseRoute);
            return View("Index", genres);
        }

        // GET: genre/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Genre genre = await apiService.GetAsync<Genre>(baseRoute + id);
            return View("Details", genre);
        }

        // GET: genre/Create
        public ActionResult Create()
        {
            return View("Create", new Genre());
        }

        // POST: genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name")] Genre genre)
        {
            try
            {
                await apiService.PostAsync<Genre>(baseRoute + genre.Id, genre);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: genre/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Genre genre = await apiService.GetAsync<Genre>(baseRoute + id);
            return View("Edit", genre);
        }

        // POST: genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Genre genre)
        {
            try
            {
                await apiService.PutAsync<Genre>(baseRoute + genre.Id, genre);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: genre/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Genre genre = await apiService.GetAsync<Genre>(baseRoute + id);
            return View("Delete", genre);
        }

        // POST: genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<Genre>(baseRoute + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
