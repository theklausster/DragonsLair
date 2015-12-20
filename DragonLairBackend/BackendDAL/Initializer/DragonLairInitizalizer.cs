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
        private Player player4;
        private Player player5;
        private Player player6;
        private Player player7;
        private Player player8;
        private Player player9;
        private Player player10;
        private Player player11;
        private Player player12;
        private Player player13;
        private Group group1;
        private Tournament tournament;
        private Team team;
        private Team team2;
        private TournamentType tournamentType1;
        private TournamentType tournamentType2;
        private Genre genre;
        private Game game1;
        private Match match;
  
        public DragonLairInitizalizer()
        {

            genre = new Genre() { Name = "Roleplaying" };
            game1 = new Game() { Name = "Wars", Genre = genre };
            tournamentType1 = new TournamentType() { Type = "1vs1" };
            tournamentType2 = new TournamentType() { Type = "2vs2" };
            player1 = new Player() { Name = "Søren" };
            player2 = new Player() { Name = "Mark" };
            player3 = new Player() { Name = "René" };
            player4 = new Player() { Name = "Ulla" };
            player5 = new Player() { Name = "Jim" };
            player6 = new Player() { Name = "Glenn" };
            player7 = new Player() { Name = "Klaus" };
            player8 = new Player() { Name = "Signe" };
            player9 = new Player() { Name = "Emilie" };
            player10 = new Player() { Name = "Bobby" };
            player11 = new Player() { Name = "Emma" };
            player12 = new Player() { Name = "Lars" };
            player13 = new Player() { Name = "Henrik" };
        

            team = new Team() { Name = "Team", Loss = 0, Win = 0, Draw = 0, Players = new List<Player> { player1, player2 } };
            team2 = new Team() { Name = "Team2", Loss = 0, Win = 0, Draw = 0, Players = new List<Player> { player3 } };
            group1 = new Group() { Name = "Group", Teams = new List<Team>() { team, team2 } };
            match = new Match() { Round = 1.ToString(), HomeTeam = team, AwayTeam = team2, Winner = null, Tournament = tournament };

            tournament = new Tournament() { Name = "tournament", Game = game1, Groups = new List<Group> { group1 }, TournamentType = tournamentType1, StartDate = DateTime.Today, Matches = new List<Match>() { match} };
        
        }

        protected override void Seed(DragonLairContext context)
        {
            context.Genres.Add(genre);
            context.Games.Add(game1);
            context.TournamentTypes.Add(tournamentType1);
            context.TournamentTypes.Add(tournamentType2);
            context.Players.Add(player1);
            context.Players.Add(player2);
            context.Players.Add(player3);
            context.Players.Add(player4);
            context.Players.Add(player5);
            context.Players.Add(player6);
            context.Players.Add(player7);
            context.Players.Add(player8);
            context.Players.Add(player9);
            context.Players.Add(player10);
            context.Players.Add(player11);
            context.Players.Add(player12);
            context.Players.Add(player13);
            context.Teams.Add(team);
            context.Teams.Add(team2);
            context.Groups.Add(group1);
            context.Matches.Add(match);
            context.Tournaments.Add(tournament);
           
         

            base.Seed(context);
        }
    }
}
