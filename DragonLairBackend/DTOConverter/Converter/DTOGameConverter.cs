using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOGameConverter : DTOAbstract<Game, DTOGame>
    {
        public DTOGameConverter()
        {
        }

        public override DTOGame Convert(Game t)
        {
            return new DTOGame() { Id = t.Id, Name = t.Name };
        }
    }
}