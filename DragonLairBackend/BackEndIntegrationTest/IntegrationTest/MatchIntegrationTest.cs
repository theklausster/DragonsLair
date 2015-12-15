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
    class MatchIntegrationTest
    {
        private TournamentController tournamentController = new TournamentController();
        private GroupController groupController = new GroupController();
        private TeamController teamController = new TeamController();
        private MatchController matchController;
        private Match match;
        private Tournament tournamentFromDb;
        private Team teamFromDb;
        private Group groupFromDb;

        [SetUp]
        public void SetUp()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/match");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Match" } });


            matchController = new MatchController();
            UrlHelper urlHelper = new UrlHelper(request);
            matchController.ControllerContext = new HttpControllerContext(config, routeData, request);
            matchController.Request = request;
            matchController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            matchController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            matchController.Url = urlHelper;

           
            var Response = tournamentController.Get(1);
            var ContentResult = Response as OkNegotiatedContentResult<DTOTournament>;
            DTOTournament DtoTournament = ContentResult.Content;
            tournamentFromDb = new Tournament();
            tournamentFromDb.Name = DtoTournament.Name;
            tournamentFromDb.Id = DtoTournament.Id;
            List<Group> dtoGroups = new List<Group>();
            foreach (var dtoGroup in DtoTournament.DtoGroups)
            {
                Group group = new Group();
                group.Name = dtoGroup.Name;
                group.Id = dtoGroup.Id;
                List<Team> dtoTeams = new List<Team>();
                foreach (var dtoTeam in dtoGroup.DtoTeams)
                {
                    Team team = new Team();
                    team.Name = dtoTeam.Name;
                    team.Id = dtoTeam.Id;
                    team.Draw = dtoTeam.Draw;
                    team.Loss = dtoTeam.Loss;
                    team.Win = dtoTeam.Win;
                    List<Player> dtoPlayers = new List<Player>();
                    foreach (var dtoPlayer in dtoTeam.DtoPlayers)
                    {
                        Player player = new Player();
                        player.Name = dtoPlayer.Name;
                        player.Id = dtoPlayer.Id;
                        dtoPlayers.Add(player);
                    }
                    team.Players = dtoPlayers;
                    dtoTeams.Add(team);
                }
                group.Teams = dtoTeams;
                tournamentFromDb.Groups = dtoGroups;
            }
            match = new Match() { Id = 1, Round = "Integration Test Playoff Round 1", Tournament = tournamentFromDb};

        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void Test_You_Can_Read_All_Match_From_Database()
        {
            IEnumerable<DTOMatch> matches = matchController.Get();
            Assert.IsNotEmpty(matches);
            Assert.IsNotNull(matches);
        }

        [Test]
        public void Test_You_Can_Create_A_Match_On_DataBase()
        {
            var response = matchController.Post(match);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Match testMatch = ((dynamic)task.Result);
                matchController.Delete(testMatch.Id);
                Assert.Greater(testMatch.Id, 0);


            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Match_On_Database()
        {
            var response = matchController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOMatch>;
            DTOMatch matchFromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }
        [Test]
        public void Test_You_Can_Update_A_Match_On_DataBase()
        {
            match.Id = 1;
            Match newMatch = match;
            newMatch.Round = "Integration PlayOff round 2";
            matchController.Put(match.Id, newMatch);
            var response = matchController.Get(match.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOMatch>;
            DTOMatch dtoMatch = contentResult.Content;


            Assert.AreEqual(contentResult.Content.Round, newMatch.Round);

        }

        [Test]
        public void Test_You_Can_Delete_A_Match_On_Database()
        {
            var response = matchController.Post(match);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOMatch dtoMatch = ((dynamic)task.Result);
                match.Id = dtoMatch.Id;
                matchController.Delete(match.Id);
                Assert.Greater(dtoMatch.Id, 0);

            });

           
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}