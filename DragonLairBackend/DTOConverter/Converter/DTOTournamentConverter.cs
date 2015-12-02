using System;
using System.Collections.Generic;
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
            if(t.Game == null || t.TournamentType == null || t.Groups == null) throw new ArgumentException("Missing some data");
            DTOTournament dtoTournament = new DTOTournament();
            List<DTOGroup> dtoGroups = new List<DTOGroup>();
            dtoTournament.Id = t.Id;
            dtoTournament.Name = t.Name;
            dtoTournament.StartDate = t.StartDate;
            DTOTournamentType dtoTournamentType = new DTOTournamentType() {Id = t.TournamentType.Id, Type = t.TournamentType.Type};
            dtoTournament.DTOTournamentType = dtoTournamentType;
            foreach (var group in t.Groups)
            {
                List<DTOTeam> dtoTeams = new List<DTOTeam>();
                foreach (var team in group.Teams)
                {
                    List<DTOPlayer> dtoPlayers = new List<DTOPlayer>();
                    foreach (var player in team.Players)
                    {
                        dtoPlayers.Add(new DTOPlayer() { Id = player.Id, Name = player.Name });
                    }
                    dtoTeams.Add(new DTOTeam() { Id = team.Id, Name = team.Name, Win = team.Win, Loss = team.Loss, Draw = team.Draw, DtoPlayers = dtoPlayers });
                }
                dtoGroups.Add(new DTOGroup() { Id = group.Id, Name = group.Name, DtoTeams = dtoTeams, DtoTournament = dtoTournament });
            }
            dtoTournament.DtoGroups = dtoGroups;
            dtoTournament.DtoGame = new DTOGame() {Id = t.Game.Id, Name = t.Game.Name, DtoGenre = new DTOGenre() {Id = t.Game.Genre.Id, Name = t.Game.Genre.Name} };
            return dtoTournament;
        }
    }
}