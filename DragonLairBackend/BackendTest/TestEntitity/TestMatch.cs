using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using NUnit.Framework;

namespace BackendTest.TestEntitity
{
    [TestFixture]
    class TestMatch
    {
        int id;
        string round;
        Match match;

        [SetUp]
        public void SetUp()
        {
            id = 1;
            round = "Playoff";
            match = new Match() {Id = id, Round = round};
        }

        [TearDown]
        public void TearDown()
        {
            match = null;
        }

        [Test]
        public void Test_if_attributes_can_be_set()
        {
            // Attributes is set in "SetUp"
            Group group = new Group() { Id = 1, Name = "Test group" };
            Tournament tournament = new Tournament() { Id = 1, Name = "Test tournament" };
            match.Tournament = tournament;
            Assert.AreEqual(tournament.Id, match.Tournament.Id);
            Assert.AreEqual(match.Id, id);
            Assert.AreEqual(match.Round, round);
        }
    }
}
