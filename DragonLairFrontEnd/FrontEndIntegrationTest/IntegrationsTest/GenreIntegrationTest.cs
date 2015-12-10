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
    public class GenreIntegrationTest
    {
        private Genre genre;
        private WebApiService apiService;
        private GenreController genreController;

        [SetUp]
        public void SetUp()
        {
            genre = new Genre() {Name = "TestGenre"};
            apiService = new WebApiService();
            genreController = new GenreController();
        }

        [TearDown]
        public void TearDown()
        {
            genre = null;
            apiService = null;
            genreController = null;
        }
        [Test]
        public async void Test_if_index_returns_a_view()
        {
           
            var result = await genreController.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Index", result.ViewName);
        }
        [Test]
        public async void Test_if_details_returns_a_view()
        {
            
            var result = await genreController.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Details", result.ViewName);

        }

        [Test]
        public async void Test_if_edit_returns_a_view()
        {
            var result = await genreController.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Edit", result.ViewName);
        }

        [Test]
        public void Test_if_create_returns_a_view()
        {
            var result = genreController.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Create", result.ViewName);
        }

        [Test]
        public async void Test_if_delete_returns_a_view()
        {
            var result = await genreController.Delete(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Delete", result.ViewName);
        }
    }
}