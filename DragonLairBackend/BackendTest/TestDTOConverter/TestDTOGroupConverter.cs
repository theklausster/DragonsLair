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
    class TestDTOGroupConverter
    {
        //[Test]
        //public void Test_if_Group_with_team_and_tournaments_can_be_converted_to_DTO()
        //{
        //    Player player = new Player() { Id = 1, Name = "Søren" };
        //    Player player2 = new Player() { Id = 2, Name = "Lars" };
        //    Player player3 = new Player() { Id = 3, Name = "Peter" };
        //    Player player4 = new Player() { Id = 4, Name = "Poul" };

        //    List<Player> PlayersForTeam1 = new List<Player>() { player, player2 };
        //    List<Player> PlayersForTeam2 = new List<Player>() { player3, player4 };

        //    Team Team1 = new Team() { Id = 1, Name = "Awesome", Players = PlayersForTeam1 };
        //    Team Team2 = new Team() { Id = 1, Name = "More Awesome", Players = PlayersForTeam2 };

        //    Tournament tournament1 = new Tournament()
        //    {
        //        Id = 1,
        //        Name = "Warhammer",
        //        Game = new Game()
        //        {
        //            Id = 1,
        //            Name = "Wars",
        //            Genre = new Genre() { Id = 1, Name = "role" }
        //        },
        //        StartDate = DateTime.Today,
        //        TournamentType = new TournamentType()
        //        {
        //            Id = 1,
        //            Type = "2vs2"
        //        }
        //    };
        //    List<Team> teams = new List<Team> { Team1, Team2 };

        //    Group group = new Group() { Id = 1, Name = "A", Teams = teams, Tournament = tournament1 };

        //    DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
        //    DTOGroup dtoGroup = dtogroupConverter.Convert(group);

        //    Assert.IsNotNull(teams);
        //    Assert.AreEqual(dtoGroup.Id, 1);
        //    Assert.AreSame(Team1.Name, dtoGroup.DtoTeams[0].Name);
        //    Assert.Greater(dtoGroup.DtoTeams.Count, 0);
        //    Assert.AreEqual(dtoGroup.DtoTeams[0].Id, group.Teams[0].Id);
        //    Assert.AreEqual(dtoGroup.DtoTeams[1].Id, group.Teams[1].Id);
        //    Assert.AreEqual(dtoGroup.DtoTeams[0].Name, group.Teams[0].Name);
        //    Assert.AreEqual(dtoGroup.DtoTeams[1].Name, group.Teams[1].Name);
        //    Assert.AreEqual(dtoGroup.DtoTournament.Id, tournament1.Id);
        //    Assert.AreEqual(dtoGroup.DtoTournament.Name, tournament1.Name);
        //    Assert.AreEqual(dtoGroup.DtoTournament.StartDate, tournament1.StartDate);
        //    Assert.AreEqual(dtoGroup.DtoTournament.DTOTournamentType.Id, group.Tournament.TournamentType.Id);
        //    Assert.AreEqual(dtoGroup.DtoTournament.DTOTournamentType.Type, group.Tournament.TournamentType.Type);
        //}

<<<<<<< HEAD
            Assert.IsNotNull(teams);
            Assert.AreEqual(dtoGroup.Id, 1);
            Assert.AreSame(Team1.Name, dtoGroup.DtoTeams[0].Name);
            Assert.Greater(dtoGroup.DtoTeams.Count, 0);
            Assert.AreEqual(dtoGroup.DtoTeams[0].Id, group.Teams[0].Id);
            Assert.AreEqual(dtoGroup.DtoTeams[1].Id, group.Teams[1].Id);
            Assert.AreEqual(dtoGroup.DtoTeams[0].Name, group.Teams[0].Name);
            Assert.AreEqual(dtoGroup.DtoTeams[1].Name, group.Teams[1].Name);
            Assert.AreEqual(dtoGroup.DtoTournament.Id, tournament1.Id);
            Assert.AreEqual(dtoGroup.DtoTournament.Name, tournament1.Name);
            Assert.AreEqual(dtoGroup.DtoTournament.StartDate, tournament1.StartDate);
        }
=======
>>>>>>> dffa372d7de749df43200a505dcb6e23a477d4e2
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_group_faills_if_teams_is_null()
        {

            Tournament tournament1 = new Tournament() { Id = 1, Name = "Warhammer" };
            Group group = new Group() { Id = 1, Name = "A", Tournament = tournament1 };
            DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
            DTOGroup dtogoup = dtogroupConverter.Convert(group);

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_group_faills_if_null()
        {
            Group group = null;
            DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
            DTOGroup dtogoup = dtogroupConverter.Convert(group);

        }

        [Test]
        public void Test_if_group_can_be_created_if_tournaments_is_null()
        {
            Team Team1 = new Team() { Id = 1, Name = "Awesome" };
            Team Team2 = new Team() { Id = 1, Name = "More Awesome" };
            List<Team> teams = new List<Team> { Team1, Team2 };
            Group group = new Group() { Id = 1, Name = "A", Teams = teams };
            DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
            DTOGroup dtoGroup = dtogroupConverter.Convert(group);
            Assert.IsNotNull(dtoGroup);
            Assert.AreEqual(dtoGroup.Id, group.Id);
            Assert.AreEqual(dtoGroup.Name, group.Name);
            Assert.AreEqual(dtoGroup.DtoTeams[0].Id, group.Teams[0].Id);
            Assert.AreEqual(dtoGroup.DtoTeams[1].Id, group.Teams[1].Id);
            Assert.AreEqual(dtoGroup.DtoTeams[0].Name, group.Teams[0].Name);
            Assert.AreEqual(dtoGroup.DtoTeams[1].Name, group.Teams[1].Name);
        }

<<<<<<< HEAD
        [Test]
        public void Test_if_group_can_be_created_with_a_tournaments()
        {
            Team Team1 = new Team() { Id = 1, Name = "Awesome" };
            Team Team2 = new Team() { Id = 1, Name = "More Awesome" };
            List<Team> teams = new List<Team> { Team1, Team2 };
            Tournament tournament = new Tournament() { Id = 1, Name = "tour1", Game = new Game() { Id = 1, Name = "war", Genre = new Genre() { Id = 1, Name = "role" } }, StartDate = DateTime.Today, TournamentType = new TournamentType() { Id = 1, Type = "2vs2" } };
            Group group = new Group() { Id = 1, Name = "A", Teams = teams, Tournament = tournament };
            DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
            DTOGroup dtoGroup = dtogroupConverter.Convert(group);
            Assert.IsNotNull(dtoGroup);
            Assert.AreEqual(dtoGroup.Id, group.Id);
            Assert.AreEqual(dtoGroup.Name, group.Name);
            Assert.AreEqual(dtoGroup.DtoTeams[0].Id, group.Teams[0].Id);
            Assert.AreEqual(dtoGroup.DtoTeams[1].Id, group.Teams[1].Id);
            Assert.AreEqual(dtoGroup.DtoTeams[0].Name, group.Teams[0].Name);
            Assert.AreEqual(dtoGroup.DtoTeams[1].Name, group.Teams[1].Name);
            Assert.AreEqual(dtoGroup.DtoTournament.Id, tournament.Id);
            Assert.AreEqual(dtoGroup.DtoTournament.Name, tournament.Name);
            Assert.AreEqual(dtoGroup.DtoTournament.StartDate, tournament.StartDate);
         }
=======
        //[Test]
        //public void Test_if_group_can_be_created_with_a_tournaments()
        //{
        //    Team Team1 = new Team() { Id = 1, Name = "Awesome" };
        //    Team Team2 = new Team() { Id = 1, Name = "More Awesome" };
        //    List<Team> teams = new List<Team> { Team1, Team2 };
        //    Tournament tournament = new Tournament() { Id = 1, Name = "tour1", Game = new Game() { Id = 1, Name = "war", Genre = new Genre() { Id = 1, Name = "role" } }, StartDate = DateTime.Today, TournamentType = new TournamentType() { Id = 1, Type = "2vs2" } };
        //    Group group = new Group() { Id = 1, Name = "A", Teams = teams, Tournament = tournament };
        //    DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
        //    DTOGroup dtoGroup = dtogroupConverter.Convert(group);
        //    Assert.IsNotNull(dtoGroup);
        //    Assert.AreEqual(dtoGroup.Id, group.Id);
        //    Assert.AreEqual(dtoGroup.Name, group.Name);
        //    Assert.AreEqual(dtoGroup.DtoTeams[0].Id, group.Teams[0].Id);
        //    Assert.AreEqual(dtoGroup.DtoTeams[1].Id, group.Teams[1].Id);
        //    Assert.AreEqual(dtoGroup.DtoTeams[0].Name, group.Teams[0].Name);
        //    Assert.AreEqual(dtoGroup.DtoTeams[1].Name, group.Teams[1].Name);
        //    Assert.AreEqual(dtoGroup.DtoTournament.Id, tournament.Id);
        //    Assert.AreEqual(dtoGroup.DtoTournament.Name, tournament.Name);
        //    Assert.AreEqual(dtoGroup.DtoTournament.StartDate, tournament.StartDate);
        //    Assert.AreEqual(dtoGroup.DtoTournament.DTOTournamentType.Id, group.Tournament.TournamentType.Id);
        //    Assert.AreEqual(dtoGroup.DtoTournament.DTOTournamentType.Type, group.Tournament.TournamentType.Type);
        //}
>>>>>>> dffa372d7de749df43200a505dcb6e23a477d4e2
    }
}
