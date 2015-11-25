using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Conveter
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