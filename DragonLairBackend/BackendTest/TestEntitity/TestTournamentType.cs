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
    class TestTournementType
    {


        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string type = "War Hammer";
            TournamentType tournementType = new TournamentType() { Id = id, Type = type };
            Assert.AreEqual(tournementType.Id, id);
            Assert.AreEqual(tournementType.Type, type);

        }
    }
}
