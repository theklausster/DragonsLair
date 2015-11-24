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
    class TestTeam
    {
        [Test]
        public void Test_if_attributes_can_be_set()
        {
            
            int id = 1;
            string name = "Team One";
            int win = 1;
            int loss = 2;
            int draw = 3;
            Team team = new Team() { Id = id, Name = name, Win = win, Loss = loss, Draw = draw };
            Assert.AreEqual(team.Id, id);
            Assert.AreEqual(team.Name, name);
            Assert.AreEqual(team.Win, win);
            Assert.AreEqual(team.Loss, loss);
            Assert.AreEqual(team.Draw, draw);
          
        }
    }
}

