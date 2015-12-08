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
    class TournamentIntegrationTest
    {
        private TournamentController tournamentController;
        private Tournament tournament;
        [SetUp]
        public void SetUp()
        {

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:41257/api/tournament");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "tournament" } });


            tournamentController = new TournamentController();
            UrlHelper urlHelper = new UrlHelper(request);
            tournamentController.ControllerContext = new HttpControllerContext(config, routeData, request);
            tournamentController.Request = request;
            tournamentController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            tournamentController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            tournamentController.Url = urlHelper;

            GroupController groupController = new GroupController();
            var response = groupController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOGroup>;
            DTOGroup DtoGroup = contentResult.Content;
            Group groupFromDb = new Group();
            groupFromDb.Name = DtoGroup.Name;
            groupFromDb.Id = DtoGroup.Id;
            List<Group> groups = new List<Group>() { groupFromDb };

            GameController gameController = new GameController();
            var gameResponse = gameController.Get(1);
            var gameContentResult = gameResponse as OkNegotiatedContentResult<DTOGame>;
            DTOGame DtoGame = gameContentResult.Content;
            Game gameFromDb = new Game();
            gameFromDb.Name = DtoGame.Name;
            gameFromDb.Id = DtoGame.Id;

            tournament = new Tournament() { Name = "Missing", Groups = groups, Game = gameFromDb };
            DbInitializer.Initialize();

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test_You_Can_Read_All_tournaments_From_Database()
        {
            IEnumerable<DTOTournament> tournaments = tournamentController.Get();
            Assert.IsNotEmpty(tournaments);
            Assert.IsNotNull(tournaments);
        }

        [Test]
        public void Test_You_Can_Create_A_Tournament_On_DataBase()
        {
            var response = tournamentController.Post(tournament);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Tournament testTournament = ((dynamic)task.Result);
                Assert.Greater(testTournament.Id, 0);

            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Tournament_On_Database()
        {
            var response = tournamentController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOTournament>;
            DTOTournament tournamentFromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }
        [Test]
        public void Test_You_Can_Update_A_Tournament_On_DataBase()
        {
            tournament.Id = 1;
            Tournament newTournament = tournament;
            newTournament.Name = "Team Test";
            tournamentController.Put(tournament.Id, newTournament);
            var response = tournamentController.Get(tournament.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOTournament>;
            DTOTournament dtoTournament = contentResult.Content;


            Assert.AreEqual(contentResult.Content.Name, newTournament.Name);

        }

        [Test]
        public void Test_You_Can_Delete_A_Tournament_On_Database()
        {
            var response = tournamentController.Post(tournament);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOTournament dtoTournament = ((dynamic)task.Result);
                tournament.Id = dtoTournament.Id;
                Assert.Greater(dtoTournament.Id, 0);

            });
            tournamentController.Delete(tournament.Id);
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}
