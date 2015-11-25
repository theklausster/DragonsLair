using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
       public class DTOTournamentConverter : DTOAbstract<Tournament, DTOTournament>
    {
        public DTOTournamentConverter()
        {
        }

        public override DTOTournament Convert(Tournament t)
        {
            return new DTOTournament() { Id = t.Id, Name = t.Name, StartDate = t.StartDate };
        }
    }
}