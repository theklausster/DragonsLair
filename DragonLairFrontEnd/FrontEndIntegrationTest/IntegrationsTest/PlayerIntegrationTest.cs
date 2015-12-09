using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using DragonLairFrontEnd.Controllers;
using DragonLairFrontEnd.Models;
using Entities;
using NUnit.Framework;
using ServiceGateway.Http;

namespace FrontEndIntegrationTest.IntegrationsTest
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
            player = new Player() { Name = "TestPlayer" };

        }

        [TearDown]
        public void TearDown()
        {
            playerController = null;
            player = null;
        }

        [Test]
        public async void Test_You_Can_Get_View_From_Controller()
        {
            var result = await playerController.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
        [Test]
        public async void Test_You_Can_Get_Players_from_view_model()
        {
            WebApiService apiService = new WebApiService();
            var player = await apiService.GetAsync<Player>("api/player/" + 1);
            PlayerModel pm = new PlayerModel();
            pm.Player = player;
            Assert.IsNotNull(pm.Player);
            Assert.AreEqual(player.Id, pm.Player.Id);
            Assert.AreEqual(player.Teams[0].Id, pm.Player.Teams[0].Id);
        }

        [Test]
        public async void Test_can_get_player_through_controller()
        {
            WebApiService apiService = new WebApiService();
            Player player = await apiService.GetAsync<Player>("api/player/" + 1);
            Assert.IsNotNull(player);
            var result = await playerController.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual("Details", result.ViewName);
        }

        [Test]
        public async void Test_Can_Create_and_delete_player_Through_controller()
        {
            var result = await playerController.Create(player);
            WebApiService apiService = new WebApiService();
            var players = await apiService.GetAsync<List<Player>>("api/player/");
            var createdePlayer = players.FirstOrDefault(a => a.Name == player.Name);
            Assert.IsNotNull(player);
            Assert.IsNotNull(players);
            Assert.IsNotNull(createdePlayer);
            var newPlayer = await apiService.GetAsync<Player>("api/player/" + createdePlayer.Id);
            Assert.AreEqual(newPlayer.Name, createdePlayer.Name);
            Assert.AreEqual(newPlayer.Id, createdePlayer.Id);
            await playerController.DeleteConfirmed(newPlayer.Id);
        }

        [Test]
        public async void Test_if_edit_return_edit_view()
        {
            WebApiService apiService = new WebApiService();
            var result = await playerController.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual("Edit", result.ViewName);
        }

        [Test]
        public async void Test_if_create_return_edit_view()
        {
            WebApiService apiService = new WebApiService();
            var result = playerController.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public async void Test_if_a_player_can_be_edited()
        {
            var result = await playerController.Create(player);
            string newName = "changedName";
            WebApiService apiService = new WebApiService();
            var players = await apiService.GetAsync<List<Player>>("api/player/");
            var createdePlayer = players.FirstOrDefault(a => a.Name == player.Name);
            Assert.IsNotNull(player);
            Assert.IsNotNull(players);
            Assert.IsNotNull(createdePlayer);
            var testPlayer = await apiService.GetAsync<Player>("api/player/" + createdePlayer.Id);
            testPlayer.Name = newName;
            string[] teamId = null;
            await playerController.Edit(testPlayer, teamId);
            var changedPlayer = await apiService.GetAsync<Player>("api/player/" + testPlayer.Id);
            Assert.AreEqual(newName, changedPlayer.Name);
            Assert.AreEqual(testPlayer.Id, changedPlayer.Id);
            await playerController.DeleteConfirmed(testPlayer.Id);
        }
    }
}
