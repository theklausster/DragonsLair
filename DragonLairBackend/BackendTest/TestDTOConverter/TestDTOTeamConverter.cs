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
    class TestDTOTeamConverter
    {

        [Test]
        public void Test_if_Team_can_be_converted_to_DTO()
        {
            DTOTeam dtoTeam = new DTOTeam();
            Team team = new Team() { Id = 1, Draw = 0, Loss = 0, Win = 1, Name = "Team Awesome" };
            DTOTeamConverter dtoTeamConverter = new DTOTeamConverter();
            dtoTeam = dtoTeamConverter.Convert(team);
            Assert.AreEqual(team.Id, dtoTeam.Id);
        }
    }
}
