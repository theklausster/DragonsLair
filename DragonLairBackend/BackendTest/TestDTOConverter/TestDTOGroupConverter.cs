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
        [Test]
        public void Test_if_Group_with_team_and_tournaments_can_be_converted_to_DTO()
        {
            Team Team1 = new Team() { Id = 1, Name = "Awesome"};
            Team Team2 = new Team() { Id = 1, Name = "More Awesome" };
            Tournament tournament1 = new Tournament() { Id = 1, Name = "Warhammer" };
            List<Team> teams = new List<Team> { Team1, Team2 };
            Group group = new Group() { Id = 1, Name = "A", Teams = teams, Tournament = tournament1 };
            DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
            DTOGroup dtogroup = dtogroupConverter.Convert(group);
            Assert.IsNotNull(teams);
            Assert.AreEqual(dtogroup.Id, 1);
            Assert.AreSame(Team1.Name, dtogroup.DtoTeams[0].Name);
            Assert.Greater(dtogroup.DtoTeams.Count, 0);
        }
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
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_group_faills_if_tournaments_is_null()
        {
            Team Team1 = new Team() { Id = 1, Name = "Awesome" };
            Team Team2 = new Team() { Id = 1, Name = "More Awesome" };
            List<Team> teams = new List<Team> { Team1, Team2 };
            Group group = new Group() { Id = 1, Name = "A", Teams = teams};
            DTOGroupConverter dtogroupConverter = new DTOGroupConverter();
            DTOGroup dtogoup = dtogroupConverter.Convert(group);

        }
    }
}
