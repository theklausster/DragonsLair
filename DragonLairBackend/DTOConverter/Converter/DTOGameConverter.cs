using System;
using System.Collections.Generic;
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
            
            DTOGenre dtoGenre = null;
            if (t == null) throw new ArgumentException("Game is not allowed to be null");
            if(t.Genre == null) throw new ArgumentException("A game must have a genre attached!");
            if(t.Tournaments != null)
            {
                List<DTOTournament> dtoTournaments = new List<DTOTournament>();
                foreach (var tournament in t.Tournaments)
                {
                    DTOTournament dtoTournament = new DTOTournament(){ Id = tournament.Id, Name = tournament.Name, StartDate = tournament.StartDate};
                    dtoTournaments.Add(dtoTournament);
                }
                return new DTOGame() { Id = t.Id, Name = t.Name, DtoGenre = dtoGenre, DtoTournaments = dtoTournaments };
            }
            dtoGenre = new DTOGenre() { Id = t.Genre.Id, Name = t.Genre.Name };
            return new DTOGame() { Id = t.Id, Name = t.Name, DtoGenre = dtoGenre};
        }
    }
}