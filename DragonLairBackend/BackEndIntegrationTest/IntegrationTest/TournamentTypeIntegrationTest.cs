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
    class TournamentTypeIntegrationTest
    {
        private TournamentTypeController tournamentTypeController;
        private TournamentType tournamentType;
        [SetUp]
        public void SetUp()
        {

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/tournamentType");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "tournamentType" } });


            tournamentTypeController = new TournamentTypeController();
            UrlHelper urlHelper = new UrlHelper(request);
            tournamentTypeController.ControllerContext = new HttpControllerContext(config, routeData, request);
            tournamentTypeController.Request = request;
            tournamentTypeController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            tournamentTypeController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            tournamentTypeController.Url = urlHelper;

            tournamentType = new TournamentType() { Type = "Integration Test TournamentType" };

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test_You_Can_Read_All_TournamentTypes_From_Database()
        {
            IEnumerable<DTOTournamentType> tournamentTypes = tournamentTypeController.Get();
            Assert.IsNotEmpty(tournamentTypes);
            Assert.IsNotNull(tournamentTypes);
        }

        [Test]
        public void Test_You_Can_Create_A_TournamentType_On_DataBase()
        {
            var response = tournamentTypeController.Post(tournamentType);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                TournamentType testTournamentType = ((dynamic)task.Result);
                tournamentTypeController.Delete(testTournamentType.Id);
                Assert.Greater(testTournamentType.Id, 0);
                

            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_TournamentType_On_Database()
        {
            var response = tournamentTypeController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOTournamentType>;
            DTOTournamentType tournamentTypeFromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }
        [Test]
        public void Test_You_Can_Update_A_TournamentType_On_DataBase()
        {
            tournamentType.Id = 1;
            TournamentType newTournamentType = tournamentType;
            newTournamentType.Type = "Integration tournamentType update";
            tournamentTypeController.Put(tournamentType.Id, newTournamentType);
            var response = tournamentTypeController.Get(tournamentType.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOTournamentType>;
            DTOTournamentType dtoTournamentType = contentResult.Content;


            Assert.AreEqual(contentResult.Content.Type, newTournamentType.Type);

        }

        [Test]
        public void Test_You_Can_Delete_A_TournamentType_On_Database()
        {
            var response = tournamentTypeController.Post(tournamentType);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOTournamentType dtoTournamentType = ((dynamic)task.Result);
                tournamentType.Id = dtoTournamentType.Id;
                tournamentTypeController.Delete(tournamentType.Id);
                Assert.Greater(dtoTournamentType.Id, 0);

            });
      
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}
