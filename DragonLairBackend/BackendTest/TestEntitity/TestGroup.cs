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
    class TestGroup
    {
        int id = 1;
        string name = "Grp 1";
        Group group;
        [SetUp]
        public void SetUp()
        {
            group = new Group() { Id = id, Name = name };
        }

        [TearDown]
        public void TearDown()
        {
            group = null;
        }



        [Test]
        public void Test_if_attributes_can_be_set()
        {  
            //Attributes are in "SetUp" 
            Assert.AreEqual(group.Id, id);
            Assert.AreEqual(group.Name, name);
        }

        [Test]
        public void Test_if_group_is_in_a_turney()
        {
            int id = 1;
            string name = "Tournament 1";
            DateTime startDate = DateTime.Today;
            group.Tournament = new Tournament() { Id = id, Name = name, StartDate = startDate };
            Assert.NotNull(group.Tournament);
            Assert.AreEqual(group.Tournament.Id, id);
        }

        [Test]
        public void Test_if_three_teams_can_be_added_to_one_group()
        {
            int id = 1;
            string name = "Team One";
            int win = 1;
            int loss = 2;
            int draw = 3;
            Team team = new Team() { Id = id, Name = name, Draw = draw, Loss = loss, Win = win };
            Team team2 = new Team() { Id = id, Name = name, Draw = draw, Loss = loss, Win = win };
            Team team3 = new Team() { Id = id, Name = name, Draw = draw, Loss = loss, Win = win };
            group.Teams = new List<Team> { team, team2, team3 };
            Assert.NotNull(group.Teams);
            Assert.Greater(group.Teams.Count, 0);
            Assert.AreEqual(team, group.Teams[0]);
            Assert.AreEqual(team2, group.Teams[1]);
            Assert.AreEqual(team3, group.Teams[2]);


        }
    }
}
