using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendDAL.Context;
using Entities;

namespace BackendDAL.Initializer
{
    class DragonLairInitizalizer : DropCreateDatabaseAlways<DragonLairContext>
    {
        private Player player1;
        private Player player2;
        private Player player3;
        private Group group1;
        private Tournament tournament;
        private Team team;
        private Team team2;
        private TournamentType tournamentType;
        private Genre genre;
        private Game game1;
        private Match match;
        public DragonLairInitizalizer()
        {
            genre = new Genre() { Name = "I'm Genre Roleplaying" };
            game1 = new Game() { Name = "Im a Game - Wars", Genre = genre };
            tournamentType = new TournamentType() { Type = "I'm type 2vs2" };
            player1 = new Player() { Name = "I'm player Søren" };
            player2 = new Player() { Name = "I'm player Mark" };
            player3 = new Player() { Name = "I'm player René" };
            team = new Team() { Name = "I'm a Team", Loss = 0, Win = 0, Draw = 0, Players = new List<Player> { player1, player2 } };
            team2 = new Team() { Name = "I'm a Team", Loss = 0, Win = 0, Draw = 0, Players = new List<Player> { player3 } };
            group1 = new Group() { Name = "I'm a Group", Tournament = new Tournament() { Name = "I'm a Tournament" }, Teams = new List<Team>() { team, team2 } };
            tournament = new Tournament() { Name = "I'm a Tournament", Game = game1, Groups = new List<Group> { group1 }, TournamentType = tournamentType, StartDate = DateTime.Today };
            match = new Match()
            {
                Round = "Playoff round 1",
                Group = group1,
                Teams = new List<Team>() { team, team2 },
                Tournament = tournament
            };
        }

        protected override void Seed(DragonLairContext context)
        {
            context.Genres.Add(genre);
            context.Games.Add(game1);
            context.TournamentTypes.Add(tournamentType);
            context.Players.Add(player1);
            context.Players.Add(player2);
            context.Players.Add(player3);
            context.Teams.Add(team);
            context.Teams.Add(team2);
            context.Groups.Add(group1);
            context.Tournaments.Add(tournament);
            context.Matches.Add(match);

            base.Seed(context);
        }
    }
}
