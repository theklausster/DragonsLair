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


        [Test]
        public void Test_if_attributes_can_be_set()
        {

            int id = 1;
            string name = "Game 1";
            Game game = new Game() { Id = id, Name = name };
            Assert.AreEqual(game.Id, id);
            Assert.AreEqual(game.Name, name);

        }
    }
    }
