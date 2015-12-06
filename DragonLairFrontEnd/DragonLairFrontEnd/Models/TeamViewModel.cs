using System.Collections.Generic;
using Entities;
using System.Linq;
using System;
using ServiceGateway.Http;
using System.Threading.Tasks;

namespace DragonLairFrontEnd.Models
{
    public class TeamViewModel
    {
        private WebApiService serviceGateway = new WebApiService();

        public List<Team> Teams { get; set; }
        public Team Team { get; private set; }
        public List<Player> SelectedPlayers { get; set; }
        public List<Player> AllPlayers { get; set; }
        public string ActionName { get; set; }

        public async Task PopulateData(int id = 0)
        {

            if (id == 0)
            {
                Teams = await serviceGateway.GetAsync<List<Team>>("api/Team/");
                AllPlayers = await serviceGateway.GetAsync<List<Player>>("api/Player/");
                if (SelectedPlayers == null) SelectedPlayers = new List<Player>();
                if (SelectedPlayers.Count > 0) SelectedPlayers.ForEach(selectedPlayer => AllPlayers.Remove(AllPlayers.FirstOrDefault(b => b.Id == selectedPlayer.Id)));
            }
            if (Team == null && id != 0) Team = await serviceGateway.GetAsync<Team>("api/Team/" + id);

            if (id != 0 && SelectedPlayers == null)
            {
                Team.Players.ForEach(a => a.Teams = null); // want teams to be disconnected - else attach in backend will throw error
                SelectedPlayers = Team.Players;
                if (SelectedPlayers == null) SelectedPlayers = new List<Player>();
            }

            if (AllPlayers == null && id != 0)
            {
                AllPlayers = await serviceGateway.GetAsync<List<Player>>("api/Player/");
                SelectedPlayers.ForEach(selectedPlayer => AllPlayers.Remove(AllPlayers.FirstOrDefault(DBPlayer => DBPlayer.Id == selectedPlayer.Id)));
            }

        }


        public void Add(int playerId)
        {
            var player = AllPlayers.FirstOrDefault(a => a.Id == playerId);
            if (player != null)
            {
                player.Teams = null; // want teams to be disconnected - else attach in backend will throw error
                SelectedPlayers.Add(player);
                AllPlayers.Remove(player);
            }
        }

        public void Remove(int playerId)
        {
            var player = SelectedPlayers.FirstOrDefault(a => a.Id == playerId);
            if (player != null)
            {
                player.Teams = null; // want teams to be disconnected - else attach in backend will throw error
                AllPlayers.Add(player);
                SelectedPlayers.Remove(player);
            }
        }






    }
}