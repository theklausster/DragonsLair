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
        public void Test_if_Game_can_be_converted_to_DTO()
        {
            DTOGame dtoGame = new DTOGame() { Id = 0, Name = "Game" };
            DTOGameConverter dtoGameConverter = new DTOGameConverter();
            Game game = new Game() { Id = 1, Name = "Game One" };
            dtoGame = dtoGameConverter.Convert(game);
            Assert.AreEqual(game.Id, dtoGame.Id);
        }
    }
}
