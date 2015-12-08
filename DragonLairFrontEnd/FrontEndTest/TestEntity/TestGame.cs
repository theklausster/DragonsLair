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
    class TestGame
    {
        Game game;
        [SetUp]
        public void SetUp()
        {
           game = new Game() {Id = 1, Name = "Game 1"}; 
        }

        [TearDown]
        public void TearDown()
        {
            game = null;
        }


        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string name = "Game 1";
            Assert.AreEqual(game.Id, id);
            Assert.AreEqual(game.Name, name);
        }

        [Test]
        public void Test_if_a_game_has_a_genre()
        {
            Genre genre1 = new Genre() {Id = 1, Name = "Figure Game"};
            Genre genre2 = new Genre() { Id = 2, Name = "Card Game" };
            game.Genre = genre1;
            Assert.IsNotNull(game.Genre);
            Assert.AreNotSame(genre2, game.Genre);
            Assert.AreSame(genre1, game.Genre);
        }
    }
    }
