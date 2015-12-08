using System.Collections.Generic;
using System.Web.Mvc;
using DragonLairFrontEnd.Controllers;
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
        playerController = new PlayerController();
         player = new Player() { Name = "Peter" };
            
        }

        [TearDown]
        public void TearDown()
        {
            playerController = null;
            player = null;
        }

        [Test]
        public void Test_You_Can_Read_All_Players_From_Database()
        {
            
        }

        //    [Test]
        //    public void Test_You_Can_Create_A_Player_On_DataBase()
        //    {
        //        var response = playerController.Post(player);
        //        response.Content.ReadAsAsync<object>().ContinueWith(task => {
        //            // The Task.Result property holds the whole deserialized object
        //            //string returnedToken = ((dynamic)task.Result).Token;
        //            Player testPlayer = ((dynamic)task.Result);
        //            Assert.Greater(testPlayer.Id, 0);

        //        });

        //    }
        //    [Test]
        //    public void Test_You_Can_Find_A_Single_Player_On_Database()
        //    {

        //    }



        //}
    }
}
