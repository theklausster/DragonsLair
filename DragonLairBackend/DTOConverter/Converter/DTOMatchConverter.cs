using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOMatchConverter : DTOAbstract<Match, DTOMatch>
    {
        public override DTOMatch Convert(Match t)
        {
            if (t.Tournament == null) throw new ArgumentException("Missing some data in Match");
            DTOMatch dtoMatch = new DTOMatch();
            List<DTOTeam> dtoTeams = new List<DTOTeam>();
            DTOTournament dtoTournament = new DTOTournament();
            DTOGroup dtoGroup = new DTOGroup();
            dtoMatch.Round = t.Round;
            dtoMatch.Id = t.Id;
            if (t.Winner != null)
            {
                DTOTeam dtoTeam = new DTOTeam(){Id = t.Winner.Id, Name = t.Winner.Name, Win = t.Winner.Win, Loss = t.Winner.Loss, Draw = t.Winner.Draw };
                dtoMatch.Winner = dtoTeam;
            }


            dtoTournament = new DTOTournament() { Id = t.Tournament.Id, Name = t.Tournament.Name, StartDate = t.Tournament.StartDate };
            dtoMatch.DtoTournament = dtoTournament;
            
            return dtoMatch;

        }
    }
}
