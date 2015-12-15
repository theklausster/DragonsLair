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
    class TestTeam
    {
        int id = 1;
        string name = "Team One";
        int win = 1;
        int loss = 2;
        int draw = 3;
        Team team;

        [SetUp]
        public void SetUp()
        {
            team = new Team() { Id = id, Name = name, Draw = draw, Loss = loss, Win = win };
        }

        [TearDown]
        public void TearDown()
        {
            team = null;
        }

        [Test]
        public void Test_if_attributes_can_be_set()
        {
            
            // Attributes are in "SetUp"
            Assert.AreEqual(team.Id, id);
            Assert.AreEqual(team.Name, name);
            Assert.AreEqual(team.Win, win);
            Assert.AreEqual(team.Loss, loss);
            Assert.AreEqual(team.Draw, draw);
          
        }

        [Test]
        public void Test_if_two_players_can_be_added_to_one_team()
        {
            int id = 1;
            string name = "Ole";
            Player player = new Player() { Id = id, Name = name };

            int id2 = 2;
            string name2 = "Ulla";
            Player player2 = new Player() { Id = id2, Name = name2 };

            team.Players = new List<Player>() { player, player2 };

            Assert.NotNull(team.Players);
            Assert.Greater(team.Players.Count, 0);
            Assert.NotNull(team.Players[0]);
            Assert.NotNull(team.Players[1]);
            Assert.AreEqual(player, team.Players[0]);
            Assert.AreEqual(player2, team.Players[1]);

        }
        [Test]
        public void Test_if_a_group_can_be_added_to_a_team()
        {
            int id = 1;
            string name = "Grp 1";
            Group group = new Group() { Id = id, Name = name };
            team.Groups = new List<Group>()
                {
                    new Group() { Id = 1, Name = "Team One" },
                    new Group() { Id = 2, Name = "Team Two" }
                };
            Assert.NotNull(team.Groups);
            Assert.AreEqual(team.Id, group.Id);
               
        }
    }
}

