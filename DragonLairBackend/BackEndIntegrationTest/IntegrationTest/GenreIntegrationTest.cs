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
    [TestFixture]
    class GenreIntegrationTest
    {
        private GenreController genreController;
        private Genre Genre;
        [SetUp]
        public void SetUp()
        {

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://dragonapi.devjakobsen.dk/api/genre");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Genre" } });


            genreController = new GenreController();
            UrlHelper urlHelper = new UrlHelper(request);
            genreController.ControllerContext = new HttpControllerContext(config, routeData, request);
            genreController.Request = request;
            genreController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            genreController.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            genreController.Url = urlHelper;

            Genre = new Genre() { Name = "BoardGame" };;
            DbTestInitializer.Initialize();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test_You_Can_Read_All_Genres_From_Database()
        {
            List<DTOGenre> genres = genreController.Get();
            Assert.IsNotEmpty(genres);
            Assert.IsNotNull(genres);
        }

        [Test]
        public void Test_You_Can_Create_A_Genre_On_DataBase()
        {
            var response = genreController.Post(Genre);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Genre testGenre = ((dynamic)task.Result);
                Assert.Greater(testGenre.Id, 0);

            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Genre_On_Database()
        {
            var response = genreController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOGenre>;
            DTOGenre genrefromDb = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Id, 1);
        }
        [Test]
        public void Test_You_Can_Update_A_Genre_On_DataBase()
        {
            Genre.Id = 1;
            Genre newGenre = Genre;
            newGenre.Name = "TestGenre";
            genreController.Put(Genre.Id, newGenre);
            var response = genreController.Get(Genre.Id);
            var contentResult = response as OkNegotiatedContentResult<DTOGenre>;
            DTOGenre dtoGenre = contentResult.Content;

            Assert.AreEqual(contentResult.Content.Name, newGenre.Name);

        }

        [Test]
        public void Test_You_Can_Delete_A_Genre_On_Database()
        {
            var response = genreController.Post(Genre);
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Genre testGenre = ((dynamic)task.Result);
                
                Assert.Greater(testGenre.Id, 0);

            });
            genreController.Delete(Genre.Id);
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}



