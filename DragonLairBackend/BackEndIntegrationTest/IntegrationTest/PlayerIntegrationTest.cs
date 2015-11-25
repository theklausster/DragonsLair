using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using BackendDAL.Initializer;
using DragonLairBackend;
using DragonLairBackend.Controllers;
using DTOConverter.DTOModel;
using Entities;
using NUnit.Framework;

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
            player = new Player() {Name = "Peter", Teams = null};
            DbInitializer.Initialize();
            playerController = new PlayerController();
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

            //NEED TO CREATE A REQUEST IT CAN SEND.
            var response = playerController.Post(player);
            response.Content.ReadAsAsync<object>().ContinueWith(task =>
            {
                // The Task.Result property holds the whole deserialized object
                player = ((dynamic)task.Result).Token;
                Assert.Greater(player.Id, 0);
            });

        }
        [Test]
        public void Test_You_Can_Find_A_Single_Player_On_Database()
        {

        }
    }
}
