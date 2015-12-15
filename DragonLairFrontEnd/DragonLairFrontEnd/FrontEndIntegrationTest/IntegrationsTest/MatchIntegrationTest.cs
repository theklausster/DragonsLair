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
    public class MatchIntegrationTest
    {
        private MatchController matchController;
        private Match match;
        private WebApiService apiService;

        [SetUp]
        public async void SetUp()
        {
            apiService = new WebApiService();
            matchController = new MatchController();

        }

        [TearDown]
        public void TearDown()
        {
            apiService = null;
            matchController = null;
            match = null;
        }

        [Test]
        public async void Test_if_index_returns_a_view()
        {
            match = await apiService.GetAsync<Match>("api/match/" + 1);
            var result = await matchController.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Index", result.ViewName);
        }
        [Test]
        public async void Test_if_details_returns_a_view()
        {
            match = await apiService.GetAsync<Match>("api/match/" + 1);
            var result = await matchController.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Details", result.ViewName);

        }

        [Test]
        public async void Test_if_edit_returns_a_view()
        {
            match = await apiService.GetAsync<Match>("api/match/" + 1);
            var result = await matchController.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Edit", result.ViewName);
        }


        //[Test]
        //public async void Test_if_a_match_can_be_edited_if_winner_is_null()
        //{
        //    match = await apiService.GetAsync<Match>("api/match/" + 1);
        //    int winningTeamId = match.Winner.Id;
        //    match.Winner = null;
        //    Assert.IsNull(match.Winner);
        //    await matchController.Edit(match, winningTeamId.ToString());
        //    Match editedMatch1 = await apiService.GetAsync<Match>("api/match/" + 1);
        //    Assert.IsNotNull(editedMatch1);
        //    Assert.IsNotNull(editedMatch1.Winner);
        //    }
        //[Test]
        //public async void Test_if_a_edit_match_fails_and_returns_a_view()
        //{
        //    match = await apiService.GetAsync<Match>("api/match/" + 1);
        //    match.Id = 5;
        //    var result = await matchController.Edit(match, match.Winner.Id.ToString()) as ViewResult;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Edit", result.ViewName);

        //}

        //[Test]
        //public async void Test_if_a_match_can_be_edited_if_winner_is_not_null()
        //{
        //    match = await apiService.GetAsync<Match>("api/match/" + 1);
        //    match.Winner = await apiService.GetAsync<Team>("api/team/" + 1);
        //    await matchController.Edit(match, match.Winner.Id.ToString());
        //    Match editedMatch1 = await apiService.GetAsync<Match>("api/match/" + 1);
        //    Assert.IsNotNull(editedMatch1);
        //    Assert.IsNotNull(editedMatch1.Winner);

        //    Match editedMatch2 = await apiService.GetAsync<Match>("api/match/" + 1);
        //    editedMatch2.Winner = await apiService.GetAsync<Team>("api/team/" + 2);
        //    await matchController.Edit(editedMatch2, editedMatch2.Winner.Id.ToString());
        //    Assert.IsNotNull(editedMatch2);
        //    Assert.IsNotNull(editedMatch2.Winner);
        //    Assert.AreNotEqual(1, editedMatch2.Winner.Id);

        //}
    }
}