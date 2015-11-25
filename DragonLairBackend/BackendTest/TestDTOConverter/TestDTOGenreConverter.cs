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
    class TestDTOGenreConverter
    {
        [Test]
        public void Test_if_Genre_can_be_converted_to_DTO()
        {
            DTOGenre dtoGenre = new DTOGenre();
            DTOGenreConverter dtoGenreConverter = new DTOGenreConverter();
            Genre genre = new Genre() { Id = 1, Name = "Adventure" };
            dtoGenre = dtoGenreConverter.Convert(genre);
            Assert.AreEqual(genre.Id, dtoGenre.Id);
        }
    }
}
