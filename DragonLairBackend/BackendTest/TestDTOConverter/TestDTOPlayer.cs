using DTOConverter;
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
    class TestDTOPlayer
    {
        [Test]
        public void Test_if_Player_can_be_converted_to_DTO()
        {
            Player player = new Player() { Id = 1, Name = "Ole" };
            DTOPlayerConverter dtoPlayerConverter = new DTOPlayerConverter();
            DTOPlayer dtoPlayer = dtoPlayerConverter.Convert(player);
            Assert.NotNull(dtoPlayer);
            Assert.AreEqual(dtoPlayer.Id, player.Id);
        }
    }
}
