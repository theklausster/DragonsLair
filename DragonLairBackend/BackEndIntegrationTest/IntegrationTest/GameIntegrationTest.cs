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
using Newtonsoft.Json;
using NUnit.Framework;

namespace BackEndIntegrationTest.IntegrationTest
{
    [TestFixture]
    class GameIntegrationTest
    {
        private GenreController genreController;
        private GameController gameController;
        private Game game;
        private Genre genrefromDb;

        [SetUp]
        public void SetUp()
        {

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/game");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "game" } });


            gameController = new GameController();
            UrlHelper urlHelper = new UrlHelper(request);
            gameController.ControllerContext = new HttpControllerContext(config, routeData, request);
            gameController.Request = request;
            gameController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            gameController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            gameController.Url = urlHelper;
            genreController = new GenreController();
            var response = genreController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOGenre>;

            DTOGenre DtoGenre = contentResult.Content;
            genrefromDb = new Genre();
            genrefromDb.Name = DtoGenre.Name;
            genrefromDb.Id = DtoGenre.Id;
            game = new Game() { Name = "Integration Test Game", Genre = genrefromDb };
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test_You_Can_Read_All_Game_From_Database()
        {
            IEnumerable<DTOGame> game = gameController.Get();
            Assert.IsNotEmpty(game);
            Assert.IsNotNull(game);
        }

        [Test]
        public void Test_You_Can_Create_A_Game_On_DataBase()
        {
            var response = gameController.Post(game);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOGame dtogame = ((dynamic)task.Result);
                game.Id = dtogame.Id;
                gameController.Delete(game.Id);
                Assert.Greater(dtogame.Id, 0);
            });
        }
        [Test]
        public void Test_You_Can_Find_A_Single_Game_On_Database()
        {
            if (game.Id == 0)
            {
                game.Id = 1;
            }
            var response = gameController.Get(game.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOGame>;
            DTOGame gamefromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);

        }

        [Test]
        public void Test_You_Can_Update_A_Game_On_DataBase()
        {
            DTOGame gamefromDb = new DTOGame();
            Game newGame = game;
            var responsePost = gameController.Post(game);
            responsePost.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                DTOGame dtogame = ((dynamic)task.Result);
                game.Id = dtogame.Id;

            });
            newGame.Name = "Integration game update";
            gameController.Put(game.Id, newGame);
            var response = gameController.Get(game.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOGame>;
            var dtoGame = contentResult.Content;
            gameController.Delete(game.Id);
            Assert.AreEqual(contentResult.Content.Name, newGame.Name);
        }

        [Test]
        public void Test_You_Can_Delete_A_Game_On_Database()
        {
            if (game.Id == 0)
            {
                var response = gameController.Post(game);
                response.Content.ReadAsAsync<object>().ContinueWith(task =>
                {
                    // The Task.Result property holds the whole deserialized object
                    //string returnedToken = ((dynamic)task.Result).Token;
                    DTOGame dtogame = ((dynamic)task.Result);
                    game.Id = dtogame.Id;
                });
            }
            gameController.Delete(game.Id);
        }
    }
}


