//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.SessionState;
//using DragonLairFrontEnd.Controllers;
//using DragonLairFrontEnd.Models;
//using Entities;
//using NUnit.Framework;
//using ServiceGateway.Http;

//namespace FrontEndIntegrationTest.IntegrationsTest
//{
//    [TestFixture]
//    public class TeamIntegrationTest
//    {
//        private TeamController teamController;
//        private Team team;
//        private List<Player> testList;

//        [SetUp]
//        public void SetUp()
//        {
//            teamController = new TeamController();
            

//        }

//        [TearDown]
//        public void TearDown()
//        {
//            teamController = null;
//            team = null;
//            testList = null;
//        }

//        //[Test]
//        //public async void Test_You_Can_Get_View_From_Controller()
//        //{
//        //    var context = new Mock<ControllerContext>();
//        //    context.Setup(x => x.HttpContext.Session["TeamViewModel"]).Returns(new TeamViewModel());
//        //    teamController.ControllerContext = context.Object;
//        //    var value = teamController.Session["TeamViewModel"];
//        //    var result = await teamController.Index() as ViewResult;
//        //    Assert.IsNotNull(result);
//        //    Assert.AreEqual("Index", result.ViewName);
//        //}
//        //[Test]
//        //public async void Test_You_Can_Get_Teams_from_view_model()
//        //{
//        //    WebApiService apiService = new WebApiService();
//        //    Team teamFromDb = await apiService.GetAsync<Team>("api/team/" + 1);
//        //    TeamViewModel teamViewModel = new TeamViewModel();
//        //    teamViewModel.Team = teamFromDb;
//        //    Assert.IsNotNull(teamViewModel.Team);
//        //    Assert.AreEqual(teamFromDb.Id, teamViewModel.Team.Id);
//        //    Assert.AreEqual(teamFromDb.Players[0].Id, teamViewModel.Team.Players[0].Id);
//        ////}

//        //[Test]
//        //public async void Test_can_get_detail_view_controller()
//        //{
//        //    WebApiService apiService = new WebApiService();
//        //    var result = await teamController.Details(1);
//        //    Assert.IsNotNull(result);
            
           
//        //}

//        [Test]
//        public async void Test_Can_Create_and_delete_team_Through_controller()
//        {
//            teamController = new TeamController();
//            testList = new List<Player>() { new Player() { Name = "TestPlayer" } };
//            team = new Team() { Name = "TestPlayer", Draw = 0, Loss = 0, Win = 0, Players = testList };

//            var result = await teamController.Create(team) as ViewResult;

//            WebApiService apiService = new WebApiService();
//            var teams = await apiService.GetAsync<List<Team>>("api/team/");
//            var createdTeam = teams.FirstOrDefault(a => a.Name == team.Name);
//            Assert.IsNotNull(team);
//            Assert.IsNotNull(teams);
//            Assert.IsNotNull(createdTeam);
//            var newTeam = await apiService.GetAsync<Team>("api/team/" + createdTeam.Id);
//            Assert.AreEqual(newTeam.Name, createdTeam.Name);
//            Assert.AreEqual(newTeam.Id, createdTeam.Id);
//            await teamController.DeleteConfirmed(newTeam.Id);
//        }

//        [Test]
//        public async void Test_if_edit_return_edit_view()
//        {
//            WebApiService apiService = new WebApiService();
//            var result = await teamController.Edit(1) as ViewResult;
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Model);
//            Assert.AreEqual("Edit", result.ViewName);
//        }

//        [Test]
//        public async void Test_if_create_return_view()
//        {
//            WebApiService apiService = new WebApiService();
//            var result = teamController.Create();
//            Assert.IsNotNull(result);
//            //Assert.IsNotNull(result);
//            //Assert.AreEqual("Create", result.ViewName);
//        }

//        //    [Test]
//        //    public async void Test_if_a_player_can_be_edited()
//        //    {
//        //        var result = await teamController.Create(team);
//        //        string newName = "changedName";
//        //        WebApiService apiService = new WebApiService();
//        //        var players = await apiService.GetAsync<List<Player>>("api/team/");
//        //        var createdePlayer = players.FirstOrDefault(a => a.Name == team.Name);
//        //        Assert.IsNotNull(team);
//        //        Assert.IsNotNull(players);
//        //        Assert.IsNotNull(createdePlayer);
//        //        var testPlayer = await apiService.GetAsync<Player>("api/team/" + createdePlayer.Id);
//        //        testPlayer.Name = newName;
//        //        string[] teamId = null;
//        //        await teamController.Edit(testPlayer, teamId);
//        //        var changedPlayer = await apiService.GetAsync<Player>("api/team/" + testPlayer.Id);
//        //        Assert.AreEqual(newName, changedPlayer.Name);
//        //        Assert.AreEqual(testPlayer.Id, changedPlayer.Id);
//        //        await teamController.DeleteConfirmed(testPlayer.Id);
//        //    }

//        //    [Test]
//        //    public async void Test_if_edit_with_missing_data_returns_edit_view()
//        //    {
//        //        string newName = "changedName";
//        //        WebApiService apiService = new WebApiService();
//        //        Assert.IsNotNull(team);
//        //        string[] teamId = null;
//        //        var result = await teamController.Edit(team, teamId) as ViewResult;
//        //        Assert.AreEqual("Edit", result.ViewName);

//        //    }

//        //    [Test]
//        //    public async void Test_if_a_player_with_a_team_can_be_edited()
//        //    {
//        //        var result = await teamController.Create(team);
//        //        string newName = "changedName";
//        //        WebApiService apiService = new WebApiService();
//        //        var players = await apiService.GetAsync<List<Player>>("api/team/");
//        //        var createdePlayer = players.FirstOrDefault(a => a.Name == team.Name);
//        //        Assert.IsNotNull(team);
//        //        Assert.IsNotNull(players);
//        //        Assert.IsNotNull(createdePlayer);
//        //        var testPlayer = await apiService.GetAsync<Player>("api/team/" + createdePlayer.Id);
//        //        testPlayer.Name = newName;
//        //        string[] teamId = new string[] { "1" };
//        //        await teamController.Edit(testPlayer, teamId);
//        //        var changedPlayer = await apiService.GetAsync<Player>("api/team/" + testPlayer.Id);
//        //        Assert.AreEqual(newName, changedPlayer.Name);
//        //        Assert.AreEqual(testPlayer.Id, changedPlayer.Id);
//        //        Assert.AreEqual(testPlayer.Teams[0].Id, changedPlayer.Teams[0].Id);
//        //        await teamController.DeleteConfirmed(testPlayer.Id);
//        //    }

//        //    [Test]
//        //    public async void Test_if_create_with_missing_data_returns_create_view()
//        //    {
//        //        team = null;
//        //        var result = await teamController.Create(team) as ViewResult;
//        //        Assert.AreEqual("Create", result.ViewName);

//        //    }
//        //    [Test]
//        //    public async void Test_if_delete_returns_delete_view()
//        //    {
//        //        int id = 1;
//        //        var result = await teamController.Delete(id) as ViewResult;
//        //        Assert.AreEqual("Delete", result.ViewName);
//        //    }

//        //    [Test]
//        //    public async void Test_if_a_team_can_be_removed_from_a_player()
//        //    {
//        //        WebApiService apiService = new WebApiService();
//        //        await teamController.Create(this.team);
//        //        var players = await apiService.GetAsync<List<Player>>("api/team/");
//        //        var playerWithOutTeam = players.FirstOrDefault(a => a.Name == this.team.Name);
//        //        List<Player> testList = new List<Player>() { playerWithOutTeam };
//        //        Team team = new Team() { Name = "TestTeam", Draw = 0, Loss = 0, Win = 0, Players = testList };
//        //        await apiService.PostAsync("api/team/", team);
//        //        Assert.IsNotNull(this.team);
//        //        Assert.IsNotNull(players);
//        //        var playerlist = await apiService.GetAsync<List<Player>>("api/team/");
//        //        var createdePlayer = playerlist.FirstOrDefault(a => a.Name == this.team.Name);
//        //        Assert.IsNotNull(createdePlayer);
//        //        int teamSizeBefore = createdePlayer.Teams.Count;
//        //        int teamIdToRemove = createdePlayer.Teams[0].Id;
//        //        await teamController.Remove(createdePlayer.Teams[0].Id, createdePlayer.Id);
//        //        Player playerAfter = await apiService.GetAsync<Player>("api/team/" + createdePlayer.Id);
//        //        int teamSizeAfter = playerAfter.Teams.Count;
//        //        Assert.AreNotEqual(teamSizeAfter, teamSizeBefore);
//        //        await apiService.DeleteAsync<Team>("api/team/" + teamIdToRemove);
//        //        await teamController.DeleteConfirmed(createdePlayer.Id);
//        //    }

//        //    [Test]
//        //    public async void Test_if_a_team_can_be_added_to_a_player()
//        //    {
//        //        Player player1 = new Player() { Name = "TestPlayer1" };
//        //        Player player2 = new Player() { Name = "Testplayer2" };
//        //        WebApiService apiService = new WebApiService();
//        //        await teamController.Create(player1);
//        //        await teamController.Create(player2);
//        //        var players = await apiService.GetAsync<List<Player>>("api/team/");
//        //        var player1WithOutTeam = players.FirstOrDefault(a => a.Name == player1.Name);
//        //        var player2WithOutTeam = players.FirstOrDefault(a => a.Name == player2.Name);
//        //        List<Player> testList1 = new List<Player>() { player1WithOutTeam };
//        //        List<Player> testList2 = new List<Player>() { player2WithOutTeam };
//        //        Team teamOne = new Team() { Name = "TestTeam1", Draw = 0, Loss = 0, Win = 0, Players = testList1 };
//        //        Team teamTwo = new Team() { Name = "TestTeam2", Draw = 0, Loss = 0, Win = 0, Players = testList2 };
//        //        await apiService.PostAsync("api/team/", teamOne);
//        //        await apiService.PostAsync("api/team/", teamTwo);
//        //        var playerlist = await apiService.GetAsync<List<Player>>("api/team/");
//        //        var createdePlayer1 = playerlist.FirstOrDefault(a => a.Name == player1.Name);
//        //        var createdePlayer2 = playerlist.FirstOrDefault(a => a.Name == player2.Name);

//        //        Assert.IsNotNull(createdePlayer1);
//        //        Assert.IsNotNull(createdePlayer2);
//        //        int teamSizeBefore = createdePlayer1.Teams.Count;
//        //        int teamId1ToRemove = createdePlayer1.Teams[0].Id;
//        //        int teamId2ToRemove = createdePlayer2.Teams[0].Id;
//        //        Team teamToAdd = await apiService.GetAsync<Team>("api/team/" + createdePlayer2.Teams[0].Id);

//        //        await teamController.Add(teamToAdd.Id, createdePlayer1.Id);
//        //        Player playerAfter = await apiService.GetAsync<Player>("api/team/" + createdePlayer1.Id);
//        //        int teamSizeAfter = playerAfter.Teams.Count;
//        //        Assert.AreNotEqual(teamSizeAfter, teamSizeBefore);
//        //        await apiService.DeleteAsync<Team>("api/team/" + teamId1ToRemove);
//        //        await apiService.DeleteAsync<Team>("api/team/" + teamId2ToRemove);
//        //        await teamController.DeleteConfirmed(createdePlayer1.Id);
//        //        await teamController.DeleteConfirmed(createdePlayer2.Id);
//        //    }

//    }
//}
