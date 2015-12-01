using Entities;
using ServiceGateway.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DragonLairFrontEnd.Models;

namespace DragonLairFrontEnd.Controllers
{
  
    public class GroupController : Controller
    {
        private string baseRoute = "api/Group/";
        private WebApiService apiService = new WebApiService();

        private async Task<GroupModel> SetUpGroupModel()
        {
            List<Group> groups = await apiService.GetAsync<List<Group>>(baseRoute);
            List<Team> teams = await apiService.GetAsync<List<Team>>("api/Team/");
            List<Tournament> tournaments = await apiService.GetAsync<List<Tournament>>("api/Tournament/");
            GroupModel groupModel = new GroupModel();
            groupModel.Groups = groups;
            groupModel.Teams = teams;
            groupModel.Tournaments = tournaments;
            return groupModel;
        }
        // GET: Group
        public async Task<ActionResult> Index()
        {
            GroupModel groupModel = await SetUpGroupModel();
            return View(groupModel);
        }

        // GET: Group/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Group group = await apiService.GetAsync<Group>(baseRoute + id);
            return View(group);
        }

        // GET: Group/Create
        public async Task<ActionResult> Create()
        {
            GroupModel groupModel = await SetUpGroupModel();
            return View(groupModel);
        }

        // POST: Group/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name")] Group group, string Teams)
        {
            try
            {
                await apiService.PostAsync<Group>(baseRoute + group.Id, group);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Group group = await apiService.GetAsync<Group>(baseRoute + id);
            return View(group);
        }

        // POST: Group/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Group group)
        {
            try
            {
                await apiService.PutAsync<Group>(baseRoute + group.Id, group);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Group group = await apiService.GetAsync<Group>(baseRoute + id);
            return View(group);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await apiService.DeleteAsync<Group>(baseRoute + id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
