﻿using System;
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
        private GroupController groupController;
        private Group group;
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

            TeamController teamController = new TeamController();
            var response = teamController.Get(1);
            var contentResult = response as OkNegotiatedContentResult<DTOTeam>;
            DTOTeam DtoTeam = contentResult.Content;
            Team teamFromDb = new Team();
            teamFromDb.Name = DtoTeam.Name;
            teamFromDb.Id = DtoTeam.Id;
            List<Team> teams = new List<Team>() { teamFromDb };
            group = new Group() { Name = "g1", Teams = teams };
            DbTestInitializer.Initialize();
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
            response.Content.ReadAsAsync<object>().ContinueWith(task => {
                // The Task.Result property holds the whole deserialized object
                //string returnedToken = ((dynamic)task.Result).Token;
                Group testGroup = ((dynamic)task.Result);
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
            newplayer.Name = "Group c";
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
                Assert.Greater(dtoGroup.Id, 0);

            });
            groupController.Delete(group.Id);
            //Assert.Throws(typeof(ArgumentException), new TestDelegate(gameController.Get(game.Id)));


            //Assert.Throws(<Exception> (() => gameController.Get(game.Id));
            //Assert.Throws<ArgumentException>(gameController.Get(game.Id));


        }
    }
}
