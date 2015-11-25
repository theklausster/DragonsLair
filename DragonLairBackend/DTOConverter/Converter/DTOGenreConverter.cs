using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOGenreConverter : DTOAbstract<Genre, DTOGenre>
    {
        public DTOGenreConverter()
        {
        }

        public override DTOGenre Convert(Genre t)
        {
            return new DTOGenre() { Id = t.Id, Name = t.Name };
        }
    }
}