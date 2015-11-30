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
    class TestDTOGameConverter
    {
        [Test]
        public void Test_if_Game_with_genre_and_tournaments_can_be_converted_to_DTO()
        {
            Genre genre = new Genre() { Id = 1, Name = "Figur", Games = new List<Game>()};          
            Tournament tournament1 = new Tournament() {Id = 1, Name = "Warhammer"};
            Tournament tournament2 = new Tournament() { Id = 1, Name = "Warhammer" };
            List<Tournament> tournies = new List<Tournament> {tournament1, tournament2};
            Game game = new Game() { Id = 1, Name = "Warhammer", Tournaments = tournies, Genre = genre };
            DTOGameConverter dtogameConverter = new DTOGameConverter();
            DTOGame dtogame = dtogameConverter.Convert(game);
            Assert.IsNotNull(tournies);
            Assert.AreEqual(dtogame.Id, genre.Id);
            Assert.AreSame(tournament1.Name, dtogame.DtoTournaments[0].Name);
            Assert.Greater(dtogame.DtoTournaments.Count, 0);
        }
        [Test]
        public void Test_if_game_can_be_created_without_Tournaments()
        {       
            Genre genre = new Genre() {Id = 1, Name = "Figur"};
            Game game = new Game() { Id = 1, Name = "Warhammer", Genre = genre};
            DTOGameConverter dtogameConverter = new DTOGameConverter();
            DTOGame dtogame = dtogameConverter.Convert(game);
            Assert.IsNull(dtogame.DtoTournaments);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_game_faills_if_null()
        {
            Game game = null;
            DTOGameConverter dtogameConverter = new DTOGameConverter();
            DTOGame dtogame = dtogameConverter.Convert(game);

        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_game_faills_if_gerne_is_null()
        {
            Game game = null;
            DTOGameConverter dtogameConverter = new DTOGameConverter();
            DTOGame dtogame = dtogameConverter.Convert(game);

        }
    }
}
