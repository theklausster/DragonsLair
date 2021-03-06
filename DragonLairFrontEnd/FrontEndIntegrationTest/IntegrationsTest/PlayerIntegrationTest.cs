﻿using System.Collections.Generic;
using System.Linq;
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
        public async void Test_if_create_return_view()

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

        [Test]
        public async void Test_if_edit_with_missing_data_returns_edit_view()
        {
            string newName = "changedName";
            WebApiService apiService = new WebApiService();
            Assert.IsNotNull(player);
            string[] teamId = null;
            var result = await playerController.Edit(player, teamId) as ViewResult;
            Assert.AreEqual("Edit", result.ViewName);

        }

        [Test]
        public async void Test_if_a_player_with_a_team_can_be_edited()
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
            string[] teamId = new string[] { "1" };
            await playerController.Edit(testPlayer, teamId);
            var changedPlayer = await apiService.GetAsync<Player>("api/player/" + testPlayer.Id);
            Assert.AreEqual(newName, changedPlayer.Name);
            Assert.AreEqual(testPlayer.Id, changedPlayer.Id);
            Assert.AreEqual(testPlayer.Teams[0].Id, changedPlayer.Teams[0].Id);
            await playerController.DeleteConfirmed(testPlayer.Id);
        }

        [Test]
        public async void Test_if_create_with_missing_data_returns_create_view()
        {
            player = null;
            var result = await playerController.Create(player) as ViewResult;
            Assert.AreEqual("Create", result.ViewName);

        }
        [Test]
        public async void Test_if_delete_returns_delete_view()
        {
            int id = 1;
            var result = await playerController.Delete(id) as ViewResult;
            Assert.AreEqual("Delete", result.ViewName);
        }

        [Test]
        public async void Test_if_a_team_can_be_removed_from_a_player()
        {
            WebApiService apiService = new WebApiService();
            await playerController.Create(player);
            var players = await apiService.GetAsync<List<Player>>("api/player/");
            var playerWithOutTeam = players.FirstOrDefault(a => a.Name == player.Name);
            List<Player> testList = new List<Player>() { playerWithOutTeam };
            Team team = new Team() { Name = "TestTeam", Draw = 0, Loss = 0, Win = 0, Players = testList };
            await apiService.PostAsync("api/team/", team);
            Assert.IsNotNull(player);
            Assert.IsNotNull(players);
            var playerlist = await apiService.GetAsync<List<Player>>("api/player/");
            var createdePlayer = playerlist.FirstOrDefault(a => a.Name == player.Name);
            Assert.IsNotNull(createdePlayer);
            int teamSizeBefore = createdePlayer.Teams.Count;
            int teamIdToRemove = createdePlayer.Teams[0].Id;
            await playerController.Remove(createdePlayer.Teams[0].Id, createdePlayer.Id);
            Player playerAfter = await apiService.GetAsync<Player>("api/player/" + createdePlayer.Id);
            int teamSizeAfter = playerAfter.Teams.Count;
            Assert.AreNotEqual(teamSizeAfter, teamSizeBefore);
            await apiService.DeleteAsync<Team>("api/team/" + teamIdToRemove);
            await playerController.DeleteConfirmed(createdePlayer.Id);
        }

        [Test]
        public async void Test_if_a_team_can_be_added_to_a_player()
        {
            Player player1 = new Player() {Name = "TestPlayer1"};
            Player player2 = new Player() {Name = "Testplayer2"};
            WebApiService apiService = new WebApiService();
            await playerController.Create(player1);
            await playerController.Create(player2);
            var players = await apiService.GetAsync<List<Player>>("api/player/");
            var player1WithOutTeam = players.FirstOrDefault(a => a.Name == player1.Name);
            var player2WithOutTeam = players.FirstOrDefault(a => a.Name == player2.Name);
            List<Player> testList1 = new List<Player>() { player1WithOutTeam };
            List<Player> testList2 = new List<Player>() { player2WithOutTeam };
            Team teamOne = new Team() { Name = "TestTeam1", Draw = 0, Loss = 0, Win = 0, Players = testList1 };
            Team teamTwo = new Team() { Name = "TestTeam2", Draw = 0, Loss = 0, Win = 0, Players = testList2 };
            await apiService.PostAsync("api/team/", teamOne);
            await apiService.PostAsync("api/team/", teamTwo);
            var playerlist = await apiService.GetAsync<List<Player>>("api/player/");
            var createdePlayer1 = playerlist.FirstOrDefault(a => a.Name == player1.Name);
            var createdePlayer2 = playerlist.FirstOrDefault(a => a.Name == player2.Name);

            Assert.IsNotNull(createdePlayer1);
            Assert.IsNotNull(createdePlayer2);
            int teamSizeBefore = createdePlayer1.Teams.Count;
            int teamId1ToRemove = createdePlayer1.Teams[0].Id;
            int teamId2ToRemove = createdePlayer2.Teams[0].Id;
            Team teamToAdd = await apiService.GetAsync<Team>("api/team/" + createdePlayer2.Teams[0].Id);

            await playerController.Add(teamToAdd.Id, createdePlayer1.Id);
            Player playerAfter = await apiService.GetAsync<Player>("api/player/" + createdePlayer1.Id);
            int teamSizeAfter = playerAfter.Teams.Count;
            Assert.AreNotEqual(teamSizeAfter, teamSizeBefore);
            await apiService.DeleteAsync<Team>("api/team/" + teamId1ToRemove);
            await apiService.DeleteAsync<Team>("api/team/" + teamId2ToRemove);
            await playerController.DeleteConfirmed(createdePlayer1.Id);
            await playerController.DeleteConfirmed(createdePlayer2.Id);
        }

    }
}
