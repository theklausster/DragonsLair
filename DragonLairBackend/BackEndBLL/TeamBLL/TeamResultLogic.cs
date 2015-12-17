using System.Collections.Generic;
using Entities;

namespace BackEndBLL.TeamBLL
{
    public class TeamResultLogic
    {
        public void UpdateTeamResult(List<Team> teams)
        {
            var facade = new DALFacade();
            List<Match> matches = (List<Match>)facade.GetMatchRepository().ReadAll();
            List<Match> matchesWithWinners = new List<Match>();
            foreach (var match in matches)
            {
                if (match.Winner != null)
                {
                    match.HomeTeam.Win = 0;
                    match.AwayTeam.Win = 0;
                    match.HomeTeam.Loss = 0;
                    match.AwayTeam.Loss = 0;
                    matchesWithWinners.Add(match);
                }
            }

            foreach (var team in teams)
            {
                int winCount = 0;
                int lossCount = 0;
                team.Win = 0;
                team.Loss = 0;
                foreach (var match in matchesWithWinners)
                {


                    if (team.Id == match.Winner.Id)
                    {
                        winCount++;
                        if (team.Win != winCount)
                        {
                            team.Win = winCount;
                        }
                    }
                    if (team.Id == match.AwayTeam.Id && team.Id != match.Winner.Id || team.Id == match.HomeTeam.Id && team.Id != match.Winner.Id)
                    {
                        lossCount++;
                        if (team.Loss != lossCount)
                        {
                            team.Loss = lossCount;
                        }
                    }
                    facade.GetTeamRepository().Update(team);
                }

            }
        }
    }
}