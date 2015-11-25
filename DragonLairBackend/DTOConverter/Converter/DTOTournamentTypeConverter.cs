using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOTournamentTypeConverter : DTOAbstract<TournamentType, DTOTournamentType>
    {
        public DTOTournamentTypeConverter()
        {
        }

        public override DTOTournamentType Convert(TournamentType t)
        {
            return new DTOTournamentType() { Id = t.Id, Type = t.Type };
        }
    }
}