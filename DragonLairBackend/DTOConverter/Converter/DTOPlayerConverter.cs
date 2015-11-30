using System;
using System.Collections.Generic;
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
            List<DTOTeam> dtoTeams = new List<DTOTeam>();
            DTOPlayer dtoPlayer = new DTOPlayer();
            dtoPlayer.Id = t.Id;
            dtoPlayer.Name = t.Name;
            if(t.Teams == null) return dtoPlayer;
            foreach (var team in t.Teams)
            {
                dtoTeams.Add(new DTOTeam() {Id = team.Id, Name = team.Name, Draw = team.Draw, Loss = team.Loss, Win = team.Win});

            }
            dtoPlayer.DtoTeams = dtoTeams;
            return dtoPlayer;

        }
    }
}