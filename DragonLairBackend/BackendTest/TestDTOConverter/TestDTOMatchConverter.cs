﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DTOConverter.Converter;
//using DTOConverter.DTOModel;
//using Entities;
//using NUnit.Framework;

//namespace BackendTest.TestDTOConverter
//{
//    [TestFixture]
//    class TestDTOMatchConverter
//    {
//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void Test_if_Match_can_be_converteted_to_DTO_without_extra_relations_and_fail()
//        {
//            DTOMatch dtomatch= new DTOMatch();
//            DTOMatchConverter dtoMatchConverter = new DTOMatchConverter();
//            Match match = new Match() { Id = 1, Round = "Playoff Round 1" };
//            dtomatch = dtoMatchConverter.Convert(match);

//        }


//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void Test_if_match_can_be_converted_with_a_group_and_fail()
//        {
//            DTOMatch dtomatch = new DTOMatch();
//            Group group = new Group() {Id = 1, Name = "I am a group"};
//            DTOMatchConverter dtoMatchConverter = new DTOMatchConverter();
//            //Match match = new Match() { Id = 1, Round = "Playoff Round 1", Group = group}; 
            
//            dtomatch = dtoMatchConverter.Convert(match);

//        }


//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void Test_if_match_can_be_converted_with_tournament_and_fail()
//        {
//            DTOMatch dtomatch = new DTOMatch();
//            Tournament tournament = new Tournament() { Id = 1, Name = "Test Tourny"};
//            DTOMatchConverter dtoMatchConverter = new DTOMatchConverter();
//            Match match = new Match() { Id = 1, Round = "Playoff Round 1", Tournament = tournament};

//            dtomatch = dtoMatchConverter.Convert(match);


//        }

//        [Test]
//        public void Test_if_Match_can_be_converted_with_tournament()
//        {
//            Group group = new Group() { Id = 1, Name = "I am a group" };
//            DTOMatch dtomatch = new DTOMatch();
//            Tournament tournament = new Tournament() { Id = 1, Name = "Test Tourny" };
//            List<Team> teams = new List<Team>() { new Team() { Id = 1, Name = "Test Team" }
//        , new Team() { Id = 1, Name = "Test Team" }};
//            DTOMatchConverter dtoMatchConverter = new DTOMatchConverter();
//            Match match = new Match() { Id = 1, Round = "Playoff Round 1", Tournament = tournament};

//            dtomatch = dtoMatchConverter.Convert(match);
//            Assert.AreEqual(dtomatch.Id, match.Id);
//            Assert.IsNotNull(dtomatch.DtoTournament);
//            Assert.AreEqual(dtomatch.DtoTournament.Id, tournament.Id);
//        }

////        }
////        [Test]
////        [ExpectedException(typeof(ArgumentException))]
////        public void Test_if_match_can_be_converted_with_teams_and_fail()
////        {
////            DTOMatch dtomatch = new DTOMatch();
////            List<Team> teams = new List<Team>() { new Team() { Id = 1, Name = "Test Team" }
////        , new Team() { Id = 1, Name = "Test Team" }};
//            DTOMatchConverter dtoMatchConverter = new DTOMatchConverter();
//            Match match = new Match() { Id = 1, Round = "Playoff Round 1", Teams = teams };

//            dtomatch = dtoMatchConverter.Convert(match);

//        }
//        [Test]
//        public void Test_if_Match_can_be_converted_with_group_and_tournament_and_a_teams()
//        {
//            Group group = new Group() { Id = 1, Name = "I am a group" };
//            DTOMatch dtomatch = new DTOMatch();
//            Tournament tournament = new Tournament() { Id = 1, Name = "Test Tourny" };
//            List<Team> teams = new List<Team>() { new Team() { Id = 1, Name = "Test Team" }
//        , new Team() { Id = 1, Name = "Test Team" }};
//            DTOMatchConverter dtoMatchConverter = new DTOMatchConverter();
//            //Match match = new Match() { Id = 1, Round = "Playoff Round 1", Teams = teams, Group = group, Tournament = tournament};

//            dtomatch = dtoMatchConverter.Convert(match);

//            Assert.IsNotNull(dtomatch.DtoGroup);
//            Assert.AreEqual(dtomatch.DtoGroup.Id, group.Id);
//            Assert.AreEqual(dtomatch.Id, match.Id);
//            Assert.IsNotNull(dtomatch.DtoTeams);
//            Assert.AreEqual(dtomatch.DtoTeams.Count, teams.Count);
//            Assert.IsNotNull(dtomatch.DtoTournament);
//            Assert.AreEqual(dtomatch.DtoTournament.Id, tournament.Id);
//        }
//    }
//}
