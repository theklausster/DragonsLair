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
    class TeamIntegrationTest
    {
        private TeamController teamController;
        private Team team;
        [SetUp]
        public void SetUp()
        {

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/team");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "team" } });


            teamController = new TeamController();
            UrlHelper urlHelper = new UrlHelper(request);
            teamController.ControllerContext = new HttpControllerContext(config, routeData, request);
            teamController.Request = request;
            teamController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            teamController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            teamController.Url = urlHelper;
            PlayerController playerController = new PlayerController();
            var response = playerController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOPlayer>;

            DTOPlayer Dtoplayer = contentResult.Content;
            Player playerFromDb = new Player();
            playerFromDb.Name = Dtoplayer.Name;
            playerFromDb.Id = Dtoplayer.Id;
            List<Player> players = new List<Player>() {playerFromDb};
            team = new Team() { Name = "Missing", Players = players};
            DbTestInitializer.Initialize();

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test_You_Can_Read_All_Team_From_Database()
        {
            List<DTOTeam> teams = teamController.Get();
            Assert.IsNotEmpty(teams);
            Assert.IsNotNull(teams);
        }

        [Test]
        public void Test_You_Can_Create_A_Team_On_DataBase()
        {
            var response = teamController.Post(team);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Team testTeam = ((dynamic)task.Result);
                Assert.Greater(testTeam.Id, 0);

            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Team_On_Database()
        {
            var response = teamController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOTeam>;
            DTOTeam teamFromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }
        [Test]
        public void Test_You_Can_Update_A_Team_On_DataBase()
        {
            team.Id = 1;
            Team newTeam = team;
            newTeam.Name = "Team Test";
            teamController.Put(team.Id, newTeam);
            var response = teamController.Get(team.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOTeam>;
            DTOTeam dtoTournamentType = contentResult.Content;


            Assert.AreEqual(contentResult.Content.Name, newTeam.Name);

        }

        [Test]
        public void Test_You_Can_Delete_A_Team_On_Database()
        {
            var response = teamController.Post(team);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOTeam dtoTeam = ((dynamic)task.Result);
                team.Id = dtoTeam.Id;
                Assert.Greater(dtoTeam.Id, 0);

            });
            teamController.Delete(team.Id);
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}
