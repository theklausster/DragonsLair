using DTOConverter;
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
    class TestDTOPlayerConverter
    {
        [Test]
        public void Test_if_Player_can_be_converted_to_DTO_with_teams()
        {
            List<Team> teams = new List<Team>();
            Team team1 = new Team() { Id = 1, Name = "team one", Draw = 1, Loss = 2, Win = 2 };
            Team team2 = new Team() { Id = 2, Name = "team two", Draw = 1, Loss = 2, Win = 2 };
            teams.Add(team1);
            teams.Add(team2);
            Player player = new Player() { Id = 1, Name = "Ole", Teams = teams };
            DTOPlayerConverter dtoPlayerConverter = new DTOPlayerConverter();
            DTOPlayer dtoPlayer = dtoPlayerConverter.Convert(player);
            Assert.NotNull(teams);
            Assert.NotNull(dtoPlayer.DtoTeams);
            Assert.NotNull(dtoPlayer);
            Assert.AreEqual(dtoPlayer.Id, player.Id);
            Assert.Greater(dtoPlayer.DtoTeams.Count, 0);
            Assert.Greater(teams.Count, 0);
            Assert.LessOrEqual(dtoPlayer.DtoTeams.Count, 2);
            Assert.LessOrEqual(teams.Count, 2);
        }

        [Test]
        public void Test_if_player_can_be_created_without_team()
        {
            List<Team> teams = null;
            Player player = new Player() { Id = 1, Name = "Hans", Teams = teams };
            DTOPlayerConverter dtoPlayerConverter = new DTOPlayerConverter();
            DTOPlayer dtoPlayer = dtoPlayerConverter.Convert(player);
            Assert.IsNull(teams);
            Assert.NotNull(dtoPlayer);
            Assert.AreEqual(dtoPlayer.Id, player.Id);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void Test_if_convert_fails_without_a_player()
        {
            Player player = null;
            DTOPlayerConverter dtoPlayerConverter = new DTOPlayerConverter();
            DTOPlayer dtoPlayer = dtoPlayerConverter.Convert(player);

        }


    }
}

