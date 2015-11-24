using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter
{
    public class DTOPlayerConverter : DTOAbstract<Player, DTOPlayer>
    {
        public DTOPlayerConverter() 
        {
        }

        public override DTOPlayer Convert(Player t)
        {
            return new DTOPlayer() { Id = t.Id, Name = t.Name };
        }
    }
}