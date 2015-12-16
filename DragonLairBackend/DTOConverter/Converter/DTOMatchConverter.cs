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
            //if (t.Teams == null || t.Tournament == null || t.Group == null) throw new ArgumentException("Missing some data in Match");
            DTOMatch dtoMatch = new DTOMatch();
            List<DTOTeam> dtoTeams = new List<DTOTeam>();
            DTOTournament dtoTournament = new DTOTournament();
            dtoMatch.Round = t.Round;
            dtoMatch.Id = t.Id;
            dtoMatch.HomeTeam = new DTOTeam() { Id = t.HomeTeam.Id, Name = t.HomeTeam.Name, Win = t.HomeTeam.Win, Loss = t.HomeTeam.Loss, Draw = t.HomeTeam.Draw };
            dtoMatch.AwayTeam = new DTOTeam() { Id  = t.AwayTeam.Id, Name = t.AwayTeam.Name, Win = t.AwayTeam.Win, Loss = t.AwayTeam.Loss, Draw = t.AwayTeam.Draw };
            if (t.Winner != null)
            {
                DTOTeam dtoTeam = new DTOTeam() { Id = t.Winner.Id, Name = t.Winner.Name, Win = t.Winner.Win, Loss = t.Winner.Loss, Draw = t.Winner.Draw };
                dtoMatch.Winner = dtoTeam;
            }
        

            return dtoMatch;

        }
    }
}
