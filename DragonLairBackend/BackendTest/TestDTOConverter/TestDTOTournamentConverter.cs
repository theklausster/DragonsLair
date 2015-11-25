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
        public void Test_if_Tournament_can_be_convterted_to_DTO()
        {
            DTOTournament dtoTournament = new DTOTournament();
            DTOTournamentConverter dtoTournamentConverter = new DTOTournamentConverter();
            Tournament tournament = new Tournament() { Id = 1, Name = "Tournament One", StartDate = DateTime.Today };
            dtoTournament = dtoTournamentConverter.Convert(tournament);
            Assert.AreEqual(tournament.Id, dtoTournament.Id);
        }
    }
}
