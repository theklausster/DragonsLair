using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BackendDAL.Context;
using Entities;

namespace BackendTest.TestContext
{
    [TestFixture]
    class TestContext
    {
        DBContext db;
       [SetUp]
        public void SetUp()
        {
           db = new DBContext();
        }

        [TearDown]
        public void TearDown()
        {
           
        }

        [Test]
        public void Test_Context_Give_Data()
        {
            List<Player> players = db.Players.ToList();
            Assert.IsNotNull(players);
            Assert.Greater(players.Count, 0);

        }
    }
}
