using System.Collections.Generic;
using Entities;

namespace DragonLairFrontEnd.Models
{
    public class MatchViewModel
    {
        public Match Match { get; set; }
        public List<Team> Teams { get; set; }
        public Team Winner { get; set; }
        public List<Team> FillTeams(Team homeTeam, Team awayTeam)
        {
            List<Team> teams = new List<Team>();
            teams.Add(homeTeam);
            teams.Add(awayTeam);
            return teams;
        }

    }
}