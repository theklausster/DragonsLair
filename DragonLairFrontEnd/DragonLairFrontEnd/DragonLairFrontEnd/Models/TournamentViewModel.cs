using Entities;
using ServiceGateway.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DragonLairFrontEnd.Models
{
    public class TournamentViewModel
    {
        
        private WebApiService ServiceGateway = new WebApiService();
        public List<Tournament> Tournaments { get; set; }
        public Tournament Tournament { get; set; }

        //For Player Selection
        public List<Player> Players { get; set; }
        public List<Player> SelectedPlayers { get; set; }


        //For TournamentType selection
        public TournamentType TournamentType { get; set; }
        public List<TournamentType> TournamentTypes { get; set; }
        public bool TypeIsSelected { get; set; }

        //For Game selection
        public Game Game { get; set; }
        public List<Game> Games { get; set; }
        public bool GameIsSelected { get; set; }


        public async Task PopulateIndexData()
        {
            Tournaments = await ServiceGateway.GetAsync<List<Tournament>>("api/Tournament/");
        }

        public async Task PopulateDetailsData(int id)
        {
            Tournament = await ServiceGateway.GetAsync<Tournament>("api/Tournament/" + id);
        }

        public async Task PopulateCreateData()
        {
            if (Tournament == null) Tournament = new Tournament();
            if (Players == null) Players = await ServiceGateway.GetAsync<List<Player>>("api/Player/");
            if (SelectedPlayers == null) SelectedPlayers = new List<Player>();
            if (TournamentType == null) TournamentType = new TournamentType();
            if (TournamentTypes == null) TournamentTypes = await ServiceGateway.GetAsync<List<TournamentType>>("api/tournamenttype/");
            if (Game == null) Game = new Game();
            if (Games == null) Games = await ServiceGateway.GetAsync<List<Game>>("api/Game/");
        }

        public void AddPlayer(int id)
        {
            Player player = Players.Find(a => a.Id == id);
            if (player != null)
            {
                SelectedPlayers.Add(player);
                Players.Remove(player);
            }
        }

        public void RemovePlayer(int id)
        {
            Player player = SelectedPlayers.Find(a => a.Id == id);
            if (player != null)
            {
                Players.Add(player);
                SelectedPlayers.Remove(player);
            }
        }

        public void AddTourneyType(int id)
        {
            TournamentType type = TournamentTypes.Find(a => a.Id == id);
            if (type != null) TournamentType = type;
            TypeIsSelected = true;

        }

        public void RemoveTourneyType()
        {
            TypeIsSelected = false;
            TournamentType = null;
        }

        public void AddGame(int gameId)
        {
            Game game = Games.Find(a => a.Id == gameId);
            if (game != null) Game = game;
            GameIsSelected = true;
        }

        public void RemoveGame()
        {
            GameIsSelected = false;
            Game = null;
        }


        public async Task CreateTournament(Tournament tournament)
        {
            Tournament.Name = tournament.Name;
            Tournament.StartDate = tournament.StartDate;
            AutoGenerateGroups();
            AutoGenerateTeams();
            await SaveOnDB();
        }

        private async Task SaveOnDB()
        {

            await ServiceGateway.PostAsync<Tournament>("api/Tournament/", Tournament);
        }

        public void AutoGenerateGroups()
        {
            List<Group> groups = new List<Group>();

            // To do Logic

            Tournament.Groups = groups;
        }

        public void AutoGenerateTeams()
        {
            List<Team> teams = new List<Team>();

            // To do Logic
        }

       

        public void AutoGenerateMatches()
        {
            // To do logic
        }





    }
}