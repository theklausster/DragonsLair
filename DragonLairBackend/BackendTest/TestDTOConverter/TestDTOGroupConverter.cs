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
    class TestDTOGroupConverter
    {
        [Test]
        public void Test_if_Group_can_be_converted_to_DTO()
        {
            DTOGroup dtoGroup = new DTOGroup();
            Group group = new Group() { Id = 1, Name = "Group One" };
            DTOGroupConverter dtoGroupConverter = new DTOGroupConverter();
            dtoGroup = dtoGroupConverter.Convert(group);
            Assert.AreEqual(group.Id, dtoGroup.Id);
        }
    }
}
