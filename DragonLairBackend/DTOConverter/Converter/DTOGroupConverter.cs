using System;
using System.Collections.Generic;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOGroupConverter : DTOAbstract<Group, DTOGroup>
    {
        public DTOGroupConverter()
        {
        }


        public override DTOGroup Convert(Group t)
        {
            if (t == null) throw new ArgumentException("Game is not allowed to be null");
            if (t.Teams == null) throw new ArgumentException("A group must have at least one team attached!");
            {
                List<DTOTeam> dtoTeams = new List<DTOTeam>();
                foreach (var teams in t.Teams)
                {
                    dtoTeams.Add(new DTOTeam() { Id = teams.Id, Name = teams.Name, Draw = teams.Draw, Win = teams.Win, Loss = teams.Loss });
                }
                if (t.Tournament == null) return new DTOGroup() { Id = t.Id, Name = t.Name, DtoTeams = dtoTeams };
                DTOGenre dtoGenre = new DTOGenre() { Id = t.Tournament.Game.Genre.Id, Name = t.Tournament.Game.Genre.Name };
                DTOGame dtoGame = new DTOGame() {Id = t.Tournament.Game.Id, Name = t.Tournament.Game.Name, DtoGenre = dtoGenre};
                DTOTournamentType dtoTournamentType = new DTOTournamentType() {Id = t.Tournament.TournamentType.Id, Type = t.Tournament.TournamentType.Type };
                DTOTournament dtoTournament = new DTOTournament()
                {
                    Id = t.Tournament.Id,
                    Name = t.Tournament.Name,
                    StartDate = t.Tournament.StartDate,
                    DTOTournamentType = dtoTournamentType,
                    DtoGame = dtoGame
                };
                return new DTOGroup() { Id = t.Id, Name = t.Name, DtoTeams = dtoTeams, DtoTournament = dtoTournament };
            }
        }
    }
}