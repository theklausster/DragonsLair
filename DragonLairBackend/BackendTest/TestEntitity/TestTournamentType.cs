using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.TestEntitity
{
    [TestFixture]
    class TestTournementType
    {
        TournamentType tournementType;

        [SetUp]
        public void SetUp()
        {
            tournementType = new TournamentType() { Id = 1, Type = "Play Off" };
        }

        [TearDown]
        public void TearDown()
        {
            tournementType = null;
        }


        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string type = "Play Off";
            Assert.AreEqual(tournementType.Id, id);
            Assert.AreEqual(tournementType.Type, type);
        }

        [Test]
        public void Test_if_TournamentType_can_be_in_list_of_tournaments()
        {
            Tournament tournament1 = new Tournament()
            {
                Id = 1,
                Name = "Balck Friday",
                StartDate = DateTime.Now,
                Game = new Game()
                {
                    Id = 1,
                    Name = "WarHammer",
                    Genre = new Genre() { Id = 1, Name = "Figure Game" }
                }};
            Tournament tournament2 = new Tournament()
            {
                Id = 1,
                Name = "Bloody Friday",
                StartDate = DateTime.Now,
                Game = new Game()
                {
                    Id = 1,
                    Name = "WarHammer",
                    Genre = new Genre() { Id = 1, Name = "Figure Game" }
                }
            };
            tournementType.Tournaments = new List<Tournament>() { tournament1, tournament2};
            Assert.IsNotNull(tournementType.Tournaments);
            Assert.AreSame(tournament1, tournementType.Tournaments[0]);
            Assert.AreSame(tournament2, tournementType.Tournaments[1]);
        }
    }
}
