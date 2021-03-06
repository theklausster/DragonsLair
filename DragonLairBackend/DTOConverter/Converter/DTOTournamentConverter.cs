﻿using System;
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
            if (t.Game == null || t.TournamentType == null || t.Groups == null) throw new ArgumentException("Missing some data in Tournament");
            DTOTournament dtoTournament = new DTOTournament();
            List<DTOGroup> dtoGroups = new List<DTOGroup>();
            DTOTeam dtoWinner = null;
            DTOTournamentType dtoTournamentType = new DTOTournamentType() { Id = t.TournamentType.Id, Type = t.TournamentType.Type };
            List<DTOMatch> dtoMatches = new List<DTOMatch>();
            foreach (var match in t.Matches)
            {
                var winner = match.Winner;
                if (winner == null) dtoWinner = new DTOTeam() { };
                else dtoWinner = new DTOTeam() { Id = winner.Id, Draw = winner.Draw, Loss = winner.Loss, Name = winner.Name, Win = winner.Win };
                dtoMatches.Add(new DTOMatch()
                {
                    Id = match.Id,
                    Round = match.Round,
                    Winner = dtoWinner,
                    HomeTeam = new DTOTeam() { Id = match.HomeTeam.Id, Name = match.HomeTeam.Name, Win = match.HomeTeam.Win, Loss = match.HomeTeam.Loss, Draw = match.HomeTeam.Draw },
                    AwayTeam = new DTOTeam() { Id = match.AwayTeam.Id, Name = match.AwayTeam.Name, Win = match.AwayTeam.Win, Loss = match.AwayTeam.Loss, Draw = match.AwayTeam.Draw }
            });
            }

            foreach (var group in t.Groups)
            {
                if (group.Teams == null) return dtoTournament;
                List<DTOTeam> dtoTeams = new List<DTOTeam>();
                foreach (var team in group.Teams)
                {
                    List<DTOPlayer> dtoPlayers = new List<DTOPlayer>();
                    if (team.Players == null) return dtoTournament;
                    foreach (var player in team.Players)
                    {
                        dtoPlayers.Add(new DTOPlayer() { Id = player.Id, Name = player.Name });
                    }
                    dtoTeams.Add(new DTOTeam()
                    {
                        Id = team.Id,
                        Name = team.Name,
                        Win = team.Win,
                        Loss = team.Loss,
                        Draw = team.Draw,
                        DtoPlayers = dtoPlayers
                    });
                }
                dtoGroups.Add(new DTOGroup()
                {
                    Id = group.Id,
                    Name = group.Name,
                    DtoTeams = dtoTeams,
                    DtoTournament = dtoTournament
                });
            }

            dtoTournament.DTOTournamentType = dtoTournamentType;
            dtoTournament.Id = t.Id;
            dtoTournament.Name = t.Name;
            dtoTournament.StartDate = t.StartDate;
            dtoTournament.DtoGroups = dtoGroups;
            dtoTournament.DtoGame = new DTOGame() { Id = t.Game.Id, Name = t.Game.Name, DtoGenre = new DTOGenre() { Id = t.Game.Genre.Id, Name = t.Game.Genre.Name } };
            dtoTournament.Matches = dtoMatches;
            return dtoTournament;
        }
    }
}