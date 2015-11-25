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
    class TestDTOTournamentTypeConverter
    {
        [Test]
        public void Test_if_TournamentType_can_be_converted()
        {
            DTOTournamentType dtoTournamentType = new DTOTournamentType();
            DTOTournamentTypeConverter dtoTournamentTypeConverter = new DTOTournamentTypeConverter();
            TournamentType tournamentType = new TournamentType() { Id = 1, Type = "War Hammer" };
            dtoTournamentType = dtoTournamentTypeConverter.Convert(tournamentType);
            Assert.AreEqual(tournamentType.Id, dtoTournamentType.Id);
        }
    }
}
