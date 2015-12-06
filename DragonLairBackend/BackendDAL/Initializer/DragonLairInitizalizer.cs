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
    class DragonLairInitizalizer : DropCreateDatabaseIfModelChanges<DragonLairContext>
    {
        private Player player1;
        private Player player2;
        private Player player3;
        private Group group1;
        private Tournament tournament;
        private Team team;
        private TournamentType tournamentType;
        private Genre genre;
        private Game game1;
        public DragonLairInitizalizer()
        {
            genre = new Genre() { Name = "Roleplaying" };
            game1 = new Game() {Name = "Wars", Genre = genre };
            tournamentType = new TournamentType() { Type = "2vs2" };
            List<Group> groups = new List<Group>();
            player1 = new Player() { Name = "Søren" };
            player2 = new Player() { Name = "Mark" };
            player3 = new Player() { Name = "René" };
            List<Player> players = new List<Player> { player1, player2 };
            List<Team> teams = new List<Team>();
            team = new Team() { Name = "team1", Loss = 0, Win = 0, Draw = 0, Players = players };
            teams.Add(team);
            group1 = new Group() { Name = "group1", Tournament = tournament, Teams = teams };
            groups.Add(group1);
            tournament = new Tournament() { Name = "wars", Game = game1, Groups = groups, TournamentType = tournamentType, StartDate = DateTime.Today };
        }

        protected override void Seed(DragonLairContext context)
        {
            context.Genres.Add(genre);
            context.Games.Add(game1);
            context.Players.Add(player1);
            context.Players.Add(player2);
            context.Players.Add(player3);
            context.Teams.Add(team);
            context.Groups.Add(group1);
            context.TournamentTypes.Add(tournamentType);
            context.Tournaments.Add(tournament);
            base.Seed(context);
        }
    }
}
