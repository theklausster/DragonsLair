using System;
using System.Collections.Generic;
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
            if (t.Tournaments == null) throw new ArgumentException();
            DTOTournamentType dtoTournamentType = new DTOTournamentType();
            dtoTournamentType.Id = t.Id;
            dtoTournamentType.Type = t.Type;
            List<DTOTournament> dtoTournament = new List<DTOTournament>(); 
            foreach (var tournament in t.Tournaments)
            {
                dtoTournament.Add(new DTOTournament() {Id = tournament.Id, Name = tournament.Name, StartDate = tournament.StartDate});
            }
            dtoTournamentType.DtoTournaments = dtoTournament;
            return dtoTournamentType;
        }
    }
}