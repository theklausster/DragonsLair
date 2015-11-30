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
    class TestDTOGenreConverter
    {
        [Test]
        public void Test_if_Genre_with_games_can_be_converted_to_DTO()
        {
            Game game1 = new Game() {Id = 1, Name = "Warhammer"};
            Game game2 = new Game() { Id = 1, Name = "Warhammer 40k" };
            List<Game> games = new List<Game>();
            games.Add(game1);
            games.Add(game2);
            Genre genre = new Genre() { Id = 1, Name = "Figur", Games = games };
            DTOGenreConverter dtoGenreConverter = new DTOGenreConverter();
            DTOGenre dtoGenre = dtoGenreConverter.Convert(genre);
            Assert.IsNotNull(games);         
            Assert.AreEqual(dtoGenre.Id, genre.Id);
            Assert.AreSame(game1.Name, dtoGenre.DtoGames[0].Name);
            Assert.Greater(dtoGenre.DtoGames.Count, 0);
        }
        [Test]
        public void Test_if_genre_can_be_created_without_game()
        {
            List<Game> games = null;
            Genre genre = new Genre() { Id = 1, Name = "Figur", Games = games };
            DTOGenreConverter dtoGenreConverter = new DTOGenreConverter();
            DTOGenre dtoGenre = dtoGenreConverter.Convert(genre);
            Assert.IsNull(games);
            Assert.NotNull(dtoGenre);
            Assert.AreEqual(dtoGenre.Id, genre.Id);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_genre_faills_if_null()
        {
            Genre genre = null;
            DTOGenreConverter dtoGenreConverter = new DTOGenreConverter();
            DTOGenre dtoGenre = dtoGenreConverter.Convert(genre);
            
        }
    }
}
