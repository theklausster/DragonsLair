//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Entities;
//using NUnit.Framework;

//namespace BackendTest.TestEntitity
//{
//    [TestFixture]
//    class TestMatch
//    {
//        int id;
//        string round;
//        Match match;

//        [SetUp]
//        public void SetUp()
//        {
//            id = 1;
//            round = "Playoff";
//            match = new Match() {Id = id, Round = round};
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            match = null;
//        }

//        [Test]
//        public void Test_if_attributes_can_be_set()
//        {
//            // Attributes is set in "SetUp"
//            Group group = new Group() { Id = 1, Name = "Test group" };
//            Tournament tournament = new Tournament() { Id = 1, Name = "Test tournament" };
//            match.Group = group;
//            match.Tournament = tournament;
//            Assert.AreEqual(tournament.Id, match.Tournament.Id);
//            Assert.AreEqual(group.Id, match.Group.Id);
//            Assert.AreEqual(match.Id, id);
//            Assert.AreEqual(match.Round, round);
//        }


 

//        [Test]
//        public void Test_if_Match_can_have_teams_added()
//        {
//            int id = 1;
//            string name = "Team One";
//            int win = 1;
//            int loss = 2;
//            int draw = 3;
//            Team team = new Team()
//            {
//                Id = id,
//                Name = name,
//                Win = win,
//                Loss = loss,
//                Draw = draw,
//            };
//            Team team2 = new Team()
//            {
//                Id = 2,
//                Name = name,
//                Win = win,
//                Loss = loss,
//                Draw = draw,
//            };
//            match.Teams = new List<Team>() {team, team2};
//            Assert.NotNull(match.Teams);
//            Assert.Greater(match.Teams.Count, 0);
//        }
//    }
//}
