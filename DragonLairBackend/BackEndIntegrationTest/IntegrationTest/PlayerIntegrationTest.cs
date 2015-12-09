using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
using BackendDAL.Context;

namespace BackEndIntegrationTest.IntegrationTest
{
    [TestFixture]
    class PlayerIntegrationTest
    {
        private PlayerController playerController;
        private Player player;
        [SetUp]
        public void SetUp()
        {
         
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/player");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "player" } });
    

            playerController = new PlayerController();
            UrlHelper urlHelper = new UrlHelper(request);
            playerController.ControllerContext = new HttpControllerContext(config, routeData, request);
            playerController.Request = request;
            playerController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            playerController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            playerController.Url = urlHelper;

            player = new Player() {Name = "Peter"};
            DbTestInitializer.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
           
        }

        [Test]
        public void Test_You_Can_Read_All_Players_From_Database()
        {
            List<DTOPlayer> players = playerController.Get();
            Assert.IsNotEmpty(players);
            Assert.IsNotNull(players);
        }

        [Test]
        public void Test_You_Can_Create_A_Player_On_DataBase()
        {
            var response = playerController.Post(player);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Player testPlayer = ((dynamic)task.Result);
                Assert.Greater(testPlayer.Id, 0);

            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Player_On_Database()
        {
            var response = playerController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOPlayer>;
            DTOPlayer playerfromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }
        [Test]
        public void Test_You_Can_Update_A_Player_On_DataBase()
        {
            player.Id = 1;
            Player newplayer = player;
            newplayer.Name = "Magic The Gathering";
            playerController.Put(player.Id, newplayer);
            var response = playerController.Get(player.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOPlayer>;
            DTOPlayer dtoPlayer = contentResult.Content;


            Assert.AreEqual(contentResult.Content.Name, newplayer.Name);

        }

        [Test]
        public void Test_You_Can_Delete_A_Player_On_Database()
        {
            var response = playerController.Post(player);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOPlayer dtoplayer = ((dynamic)task.Result);
                player.Id = dtoplayer.Id;
                Assert.Greater(dtoplayer.Id, 0);

            });
            playerController.Delete(player.Id);
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}
