using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BackendTest.TestEntitity
{
    [TestFixture]
    class TestPlayer
    {
        int id;
        string name;
        Player player;

        [SetUp]
        public void SetUp()
        {
            id = 1;
            name = "Dragon";
            player = new Player() { Id = id, Name = name };
        }

        [TearDown]
        public void TearDown()
        {
            player = null;
        }

        [Test]
        public void Test_if_attributes_can_be_set()
        {
             // Attributes is set in "SetUp"
            Assert.AreEqual(player.Id, id);
            Assert.AreEqual(player.Name, name);
        }

        [Test]
        public void Test_if_player_can_be_added_to_a_team()
        {
            int id = 1;
            string name = "Team One";
            int win = 1;
            int loss = 2;
            int draw = 3;
            Team team = new Team() { Id = id, Name = name, Win = win, Loss = loss, Draw = draw, Players = new List<Player>() { player } };
            Team team2 = new Team() { Id = 2, Name = name, Win = win, Loss = loss, Draw = draw, Players = new List<Player>() { player } };
            player.Teams = new List<Team>() { team, team2 };
            Assert.NotNull(player.Teams);
            Assert.Greater(player.Teams.Count, 0);
            Assert.NotNull(team.Players[0]);
            Assert.AreEqual(player, team.Players[0]);
            Assert.NotNull(team2.Players[0]);
            Assert.AreEqual(player, team2.Players[0]);


        }

    }
}
