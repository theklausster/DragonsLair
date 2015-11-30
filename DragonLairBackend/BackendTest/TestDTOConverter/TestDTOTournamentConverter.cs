using DTOConverter.Converter;
using DTOConverter.DTOModel;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.TestDTOConverter
{
    [TestFixture]
    class TestDTOTournamentConverter
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_Tournament_can_be_converteted_to_DTO_without_extra_relations_and_fail()
        {
            DTOTournament dtoTournament = new DTOTournament();
            DTOTournamentConverter dtoTournamentConverter = new DTOTournamentConverter();
            Tournament tournament = new Tournament() {Id = 1, Name = "Tournament One", StartDate = DateTime.Today};
            dtoTournament = dtoTournamentConverter.Convert(tournament);
            
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_tournament_can_be_converted_with_a_type_and_fail()
        {
            DTOTournament dtoTournament = new DTOTournament();
            TournamentType tournamentType = new TournamentType() {Id = 1, Type = "2vs2"};
            DTOTournamentConverter dtoTournamentConverter = new DTOTournamentConverter();
            Tournament tournament = new Tournament()
            {
                Id = 1,
                Name = "Tournament One",
                StartDate = DateTime.Today,
                TournamentType = tournamentType
            };
            dtoTournament = dtoTournamentConverter.Convert(tournament);
           
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_tournament_can_be_converted_with_groups_and_type_and_fail()
        {
            DTOTournament dtoTournament = new DTOTournament();
            TournamentType tournamentType = new TournamentType() {Id = 1, Type = "2vs2"};
            List<Group> groups = new List<Group>();
            Group group1 = new Group() {Id = 1, Name = "Hans og Ole"};
            Group group2 = new Group() {Id = 1, Name = "Lars og Peter"};
            groups.Add(group1);
            groups.Add(group2);
            DTOTournamentConverter dtoTournamentConverter = new DTOTournamentConverter();
            Tournament tournament = new Tournament()
            {
                Id = 1,
                Name = "Tournament One",
                StartDate = DateTime.Today,
                TournamentType = tournamentType,
                Groups = groups
            };
            dtoTournament = dtoTournamentConverter.Convert(tournament);
           

        }
        [Test]
        public void Test_if_tournament_can_be_converted_with_groups_and_type_and_a_game()
        {
            DTOTournament dtoTournament = new DTOTournament();
            TournamentType tournamentType = new TournamentType() { Id = 1, Type = "2vs2" };
            Player player1 = new Player() { Id = 1, Name = "Hans"};
            Player player2 = new Player() {Id = 2, Name = "Ole"};
            Player player3 = new Player() { Id = 3, Name = "Lars" };
            Player player4 = new Player() { Id = 4, Name = "Peter" };
            List<Player> players1 = new List<Player>();
            List<Player> players2 = new List<Player>();
            players1.Add(player1);
            players1.Add(player2);
            players2.Add(player3);
            players2.Add(player4);
            Team team1 = new Team() {Id = 1, Name = "team1", Players = players1, Win = 2, Draw = 2, Loss = 1};
            Team team2 = new Team() { Id = 1, Name = "team2", Players = players2, Win = 3, Draw = 0, Loss = 1 };
            List<Team> teams1 = new List<Team>();
            teams1.Add(team1);
            teams1.Add(team2);
            List<Group> groups = new List<Group>();
            Group group1 = new Group() { Id = 1, Name = "Hans og Ole", Teams = teams1};
            Group group2 = new Group() { Id = 1, Name = "Lars og Peter", Teams = teams1};
            groups.Add(group1);
            groups.Add(group2);
            Game game = new Game() { Id = 1, Name = "Warhammer" };
            DTOTournamentConverter dtoTournamentConverter = new DTOTournamentConverter();
            Tournament tournament = new Tournament()
            {
                Id = 1,
                Name = "Tournament One",
                StartDate = DateTime.Today,
                TournamentType = tournamentType,
                Groups = groups,
                Game = game
            };
            
            dtoTournament = dtoTournamentConverter.Convert(tournament);
            Assert.IsNotNull(tournament.TournamentType);
            Assert.AreEqual(tournament.Id, dtoTournament.Id);
            Assert.AreEqual(tournamentType.Id, dtoTournament.DTOTournamentType.Id);
            Assert.IsNotNull(groups);
            Assert.IsNotNull(dtoTournament.DtoGroups);
            Assert.AreEqual(groups.Count, dtoTournament.DtoGroups.Count);
            Assert.IsNotNull(dtoTournament.DtoGame);
            Assert.AreEqual(dtoTournament.DtoGame.Id, game.Id);
        }

    }
}