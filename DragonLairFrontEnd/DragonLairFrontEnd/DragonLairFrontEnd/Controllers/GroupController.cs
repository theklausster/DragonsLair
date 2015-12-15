using Entities;
using ServiceGateway.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DragonLairFrontEnd.Models;
using Microsoft.Ajax.Utilities;

namespace DragonLairFrontEnd.Controllers
{

    public class GroupController : Controller
    {
        private GroupModel GM = new GroupModel();
        private string baseRoute = "api/Group/";
        private WebApiService apiService = new WebApiService();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["groupModel"] == null)
            {
                Session["groupModel"] = new GroupModel();
            }
            GM = (GroupModel)Session["groupModel"];

        }

        private async Task<GroupModel> SetUpGroupModel()
        {
            var groups = await apiService.GetAsync<List<Group>>(baseRoute);
            List<Team> teams = await apiService.GetAsync<List<Team>>("api/Team/");
            GM = new GroupModel { Groups = groups, Teams = teams };
            return GM;
        }
        // GET: Group
        public async Task<ActionResult> Index()
        {
            ResetSession();
            var groups = await apiService.GetAsync<List<Group>>(baseRoute);
            return View(groups);
        }

        private void ResetSession()
        {
            Session.Clear();
        }

        // GET: Group/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Group group = await apiService.GetAsync<Group>(baseRoute + id);
            GM.Teams = new List<Team>();

            GM.Group = group;
            foreach (var team in group.Teams)
            {
                Team teamFromDb = await apiService.GetAsync<Team>("api/team/" + team.Id);
                GM.Teams.Add(teamFromDb);
            }
            return View(GM);
        }

        // GET: Group/Create
        public async Task<ActionResult> Create()
        {
            GM = await SetUpGroupModel();
            return View(GM);
        }

        // POST: Group/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name")] Group group, string[] Teams)
        {
            group.Teams = new List<Team>();
            foreach (var id in Teams)
            {
                Team team = await apiService.GetAsync<Team>("api/team/" + id);
                group.Teams.Add(team);
            }
            try
            {

                await apiService.PostAsync<Group>(baseRoute, group);

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
            if (GM.Group != null) return View(GM);

            Group group = await apiService.GetAsync<Group>(baseRoute + id);
            GM.Teams = await apiService.GetAsync<List<Team>>("api/team/");
            GM.SelectedTeams = group.Teams;
            GM.Group = group;
            GM.SetUpListWithOutAdded();

            return View(GM);
        }

        // POST: Group/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name")] Group group, string[] teamId)
        {
            group.Teams = new List<Team>();
            if (teamId != null)
            {
                foreach (var id in teamId)
                {
                    group.Teams.Add(await apiService.GetAsync<Team>("api/team/" + id));
                }
            }
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
            return View(new GroupModel() {Group=group});
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Group group = await apiService.GetAsync<Group>(baseRoute + id);
            if (group.Tournament == null)
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
            string errorMsg = "This group can not be deleted, it is part of a tournament";
            return View(new GroupModel() { Group = group ,Error = errorMsg});
        }

        public ActionResult Remove(int teamId)
        {
            GM.Remove(teamId);
            return RedirectToAction("Edit", "Group", new { id = GM.Group.Id });

        }

        public ActionResult Add(int teamId)
        {
            GM.Add(teamId);
            return RedirectToAction("Edit", "Group", new { id = GM.Group.Id });
        }

    }
}
