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
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_TournamentType_can_be_converted_and_fail()
        {
            DTOTournamentType dtoTournamentType = new DTOTournamentType();
            DTOTournamentTypeConverter dtoTournamentTypeConverter = new DTOTournamentTypeConverter();
            TournamentType tournamentType = new TournamentType() { Id = 1, Type = "War Hammer" };
            dtoTournamentType = dtoTournamentTypeConverter.Convert(tournamentType);
           }
        [Test]
        public void Test_if_TournamentType_can_be_converted_with_tournaments()
        {
            DTOTournamentType dtoTournamentType = new DTOTournamentType();
            DTOTournamentTypeConverter dtoTournamentTypeConverter = new DTOTournamentTypeConverter();
            Tournament tournament = new Tournament() { Id = 1, Name = "warAT", Game = new Game() { Id = 1, Name = "wahamm", Genre = new Genre() { Id = 1, Name = "role" } }, StartDate = DateTime.Today };
            List<Tournament> tournaments = new List<Tournament>();
            tournaments.Add(tournament);
            TournamentType tournamentType =
                new TournamentType()
                {
                    Id = 1,
                    Type = "War Hammer",
                    Tournaments = tournaments
                };
            dtoTournamentType = dtoTournamentTypeConverter.Convert(tournamentType);
            Assert.IsNotNull(dtoTournamentType.DtoTournaments);
            Assert.AreEqual(tournamentType.Id, dtoTournamentType.Id);
        }
    }
}
