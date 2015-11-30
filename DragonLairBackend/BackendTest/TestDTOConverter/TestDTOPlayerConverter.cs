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
    class TestDTOPlayerConverter
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

        [Test]
        public void Test_if_player_can_be_created_without_team()
        {
            List<Team> teams = null;
            Player player = new Player() { Id = 1, Name = "Hans", Teams = teams};
            DTOPlayerConverter dtoPlayerConverter = new DTOPlayerConverter();
            DTOPlayer dtoPlayer = dtoPlayerConverter.Convert(player);
            Assert.IsNull(teams);
            Assert.NotNull(dtoPlayer);
            Assert.AreEqual(dtoPlayer.Id, player.Id);
        }

    }
}

