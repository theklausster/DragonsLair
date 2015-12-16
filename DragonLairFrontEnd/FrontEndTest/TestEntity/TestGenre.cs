using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.TestEntitity
{
    [TestFixture]
    class TestGenre
    {
        Genre genre;
        [SetUp]
        public void SetUp()
        {
            genre = new Genre() { Id = 1, Name = "Figure Game" };
        }

        [TearDown]
        public void TearDown()
        {
            genre = null;
        }


        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string name = "Figure Game";
            Assert.AreEqual(genre.Id, id);
            Assert.AreEqual(genre.Name, name);

        }

        [Test]
        public void Test_if_a_genre_can_be_in_many_games()
        {
            Game game1 = new Game() { Id = 1, Name = "warhammer", Genre = genre };
            Game game2 = new Game() { Id = 2, Name = "warFlower", Genre = genre };
            genre.Games = new List<Game>() { game1, game2 };
            Assert.IsNotNull(genre.Games);
            Assert.AreSame(game1, genre.Games[0]);
            Assert.AreSame(game2, genre.Games[1]);
        }
    }
}
