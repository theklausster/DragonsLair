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
    class TestGroup
    {


        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string name = "Grp 1";
            Group group = new Group() { Id = id, Name = name };
            Assert.AreEqual(group.Id, id);
            Assert.AreEqual(group.Name, name);
            
        }
    }
}
