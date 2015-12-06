using System;
using DTOConverter.DTOModel;
using Entities;
using System.Collections.Generic;

namespace DTOConverter.Converter
{
    public class DTOTeamConverter : DTOAbstract<Team, DTOTeam>
    {
        public DTOTeamConverter()
        {
        }

        public override DTOTeam Convert(Team t)
        {
            if (t == null) throw new ArgumentException("Team is null in dtoTeamConverter");
            List<DTOPlayer> dtoPlayers = new List<DTOPlayer>();
            List<DTOGroup> dtoGroup = new List<DTOGroup>();
            DTOTeam dtoTeam = new DTOTeam();
            dtoTeam.Name = t.Name;
            dtoTeam.Id = t.Id;
            dtoTeam.Win = t.Win;
            dtoTeam.Draw = t.Draw;
            dtoTeam.Loss = t.Loss;
            if (t.Players == null) throw new ArgumentException("players is null in dtoTeamConverter");

            foreach (var item in t.Players)
            {
                dtoPlayers.Add(new DTOPlayer() { Id = item.Id, Name = item.Name });
            }

                 dtoTeam.Players = dtoPlayers;

            return dtoTeam;
        }
    }
}