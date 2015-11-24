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
    class TestTournament
    {
        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string name = "Tournament 1";
            DateTime startDate = DateTime.Today;


            Tournament tournament = new Tournament() { Id = id, Name = name, StartDate = startDate };
            Assert.AreEqual(tournament.Id, id);
            Assert.AreEqual(tournament.Name, name);
            Assert.AreEqual(tournament.StartDate, startDate);
        }
    }
}
