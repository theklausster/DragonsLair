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
    class TestTournament
    {
        int id = 1;
        string name = "Tournament 1";
        DateTime startDate = DateTime.Today;
        Tournament tournament;

        [SetUp]
        public void SetUp()
        {
            tournament = new Tournament() { Id = id, Name = name, StartDate = startDate };
        }

        [TearDown]
        public void TearDown()
        {
            tournament = null;
        }

        [Test]
        public void Test_if_attributes_can_be_set()
        {
            // Attributes is set in "SetUp"
            Assert.AreEqual(tournament.Id, id);
            Assert.AreEqual(tournament.Name, name);
            Assert.AreEqual(tournament.StartDate, startDate);
        }


        [Test]
        public void Test_if_tournament_can_have_two_groups()
        {
            int id = 1;
            string name = "Grp 1";
            Group group = new Group() { Id = id, Name = name };
            Group group2 = new Group() { Id = id, Name = name };
            tournament.Groups = new List<Group>() { group, group2 };
            Assert.NotNull(tournament.Groups);
            Assert.Greater(tournament.Groups.Count, 0);
            Assert.AreEqual(tournament.Groups[0], group);
            Assert.AreEqual(tournament.Groups[1], group2);

        }

        [Test]
        public void Test_if_tournament_has_a_game()
        {
            int id = 1;
            string name = "Game 1";
            Game game = new Game() { Id = id, Name = name };
            tournament.Game = game;
            Assert.NotNull(tournament.Game);
            Assert.AreEqual(tournament.Game.Id, id);
        }

        [Test]
        public void Test_if_tournament_has_a_tournamentType()
        {
            int id = 1;
            string type = "War Hammer";
            TournamentType tournementType = new TournamentType() { Id = id, Type = type };
            tournament.TournamentType = tournementType;
            Assert.NotNull(tournament.TournamentType);
            Assert.AreEqual(tournementType.Type, type);
        }
    }
}
