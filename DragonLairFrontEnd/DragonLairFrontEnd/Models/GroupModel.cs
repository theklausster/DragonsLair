using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using ServiceGateway.Http;

namespace DragonLairFrontEnd.Models
{
    public class GroupModel
    {
        public string Error { get; set; }
        public List<Group> Groups { get; set; }
        public List<Team> Teams { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public Group Group { get; set; }
        public Tournament Tournament { get; set; }
        public List<Team> SelectedTeams { get; set; }
        public List<Team> ListWithOutAdded { get; set; }

        public GroupModel()
        {
           
        }

        public void Remove(int teamId)
        {
            Team team = Group.Teams.FirstOrDefault(a => a.Id == teamId);
            ListWithOutAdded.Add(team);
            SelectedTeams.Remove(SelectedTeams.FirstOrDefault(a => a.Id == teamId));
            
        }

        public void Add(int teamId)
        {
            Team team = Teams.FirstOrDefault(a => a.Id == teamId);
            SelectedTeams.Add(team);
            ListWithOutAdded.Remove(ListWithOutAdded.FirstOrDefault(a=> a.Id == teamId));
        }

        public void SetUpListWithOutAdded()
        {
            if(ListWithOutAdded == null) ListWithOutAdded = new List<Team>();
            foreach (var team in Teams)
            {
                ListWithOutAdded.Add(team);
            }
            foreach (var team in Teams)
            {
                foreach (var tmA in SelectedTeams)
                {
                    if (team.Id == tmA.Id)
                    {
                        ListWithOutAdded.Remove(team);
                    }
                }
            }
        }
    }
}