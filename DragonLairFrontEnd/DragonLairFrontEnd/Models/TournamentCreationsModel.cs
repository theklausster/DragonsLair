using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace DragonLairFrontEnd.Models
{
    public class TournamentCreationsModel
    {
        void CalculateAmmountOfGroups(List<Team> teams)
        {
            int AmmountOfGroups = teams.Count / 6;
            List<Group> Groups = new List<Group>();
            for (int i = 0; i < AmmountOfGroups; i++)
            {
                Groups.Add(new Group());
            }
            for (int i = 0; i < teams.Count; i++)
            {

            }

        }


        void ListMatches(List<Team> TeamsBeforeBye)
        {
            if (TeamsBeforeBye.Count % 2 != 0)
            {
                TeamsBeforeBye.Add(new Team() {Name = "Bye"});
            }

            int matchAmmount = (TeamsBeforeBye.Count - 1);
            int halfSize = TeamsBeforeBye.Count / 2;

            List<Team> teams = new List<Team>();

            teams.AddRange(TeamsBeforeBye);
            teams.RemoveAt(0);

            int teamsSize = teams.Count;

            for (int match = 0; match < matchAmmount; match++)
            {
                Console.WriteLine("Match {0}", (match + 1));

                int teamIndex = match % teamsSize;

                Console.WriteLine("{0} vs {1}", teams[teamIndex], TeamsBeforeBye[0]);

                for (int index = 1; index < halfSize; index++)
                {
                    int firstTeam = (match + index % teamsSize);
                    int secondTeam = (match + teamsSize - index % teamsSize);
                    Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);
                }
            }
        }
    }
}