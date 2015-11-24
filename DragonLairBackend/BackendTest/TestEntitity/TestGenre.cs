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


        [Test]
        public void Test_if_attributes_can_be_set()
        {
            int id = 1;
            string name = "Grp 1";
            Genre genre = new Genre() { Id = id, Name = name };
            Assert.AreEqual(genre.Id, id);
            Assert.AreEqual(genre.Name, name);

        }
    }
}
