
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using BackendDAL.Initializer;
using DragonLairBackend.Controllers;
using DTOConverter.DTOModel;
using Entities;
using NUnit.Framework;

namespace BackEndIntegrationTest.IntegrationTest
{
    class GroupIntegrationTest
    {
        private TournamentController tournamentController = new TournamentController();

        private GroupController groupController;
        TeamController teamController = new TeamController();
        private Group group;
        private Team teamFromDb;
        [SetUp]
        public void SetUp()
        {

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/group");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "group" } });


            groupController = new GroupController();
            UrlHelper urlHelper = new UrlHelper(request);
            groupController.ControllerContext = new HttpControllerContext(config, routeData, request);
            groupController.Request = request;
            groupController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            groupController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            groupController.Url = urlHelper;

            var Response = tournamentController.Get(1);
            var ContentResult = Response as OkNegotiatedContentResult<DTOTournament>;
            DTOTournament DtoTournament = ContentResult.Content;
            Tournament tournamentFromDb = new Tournament();
            tournamentFromDb.Name = DtoTournament.Name;
            tournamentFromDb.Id = DtoTournament.Id;

            var response = teamController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOTeam>;
            DTOTeam DtoTeam = contentResult.Content;
            teamFromDb = new Team();
            teamFromDb.Name = DtoTeam.Name;
            teamFromDb.Id = DtoTeam.Id;
            List<Team> teams = new List<Team>() { teamFromDb };

            group = new Group() { Name = "Integration Test Group", Teams = teams, Tournament = tournamentFromDb };


        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Test_You_Can_Read_All_Groups_From_Database()
        {
            IEnumerable<DTOGroup> groups = groupController.Get();
            Assert.IsNotEmpty(groups);
            Assert.IsNotNull(groups);
        }

        [Test]
        public void Test_You_Can_Create_A_Group_On_DataBase()
        {
            var response = groupController.Post(group);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Group testGroup = ((dynamic)task.Result);
                groupController.Delete(testGroup.Id);
                Assert.Greater(testGroup.Id, 0);

            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Group_On_Database()
        {
            var response = groupController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOGroup>;
            DTOGroup groupFromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }
        [Test]
        public void Test_You_Can_Update_A_Group_On_DataBase()
        {
            group.Id = 1;
            Group newplayer = group;
            newplayer.Name = "Integration Group c";

            groupController.Put(group.Id, newplayer);
            var response = groupController.Get(group.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOGroup>;
            DTOGroup dtoGroup = contentResult.Content;


            Assert.AreEqual(contentResult.Content.Name, newplayer.Name);

        }

        [Test]
        public void Test_You_Can_Delete_A_Group_On_Database()
        {
            var response = groupController.Post(group);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOGroup dtoGroup = ((dynamic)task.Result);
                group.Id = dtoGroup.Id;
                groupController.Delete(group.Id);
                Assert.Greater(dtoGroup.Id, 0);

            });


        }
    }
}
