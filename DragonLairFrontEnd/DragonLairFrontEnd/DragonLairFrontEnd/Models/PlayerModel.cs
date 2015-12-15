using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.UI.WebControls;
using Entities;

namespace DragonLairFrontEnd.Models
{
    public class PlayerModel
    {
        public Player Player { get; set; }
        public List<Team> Teams { get; set; }

        public List<Team> TeamsAdded { get; set; }

        public List<Team> ListWithOutAdded { get; set; }

        public PlayerModel()
        {
            
        }

        public void SetupList(Player player, List<Team> listOfTeams )
        {

            ListWithOutAdded = new List<Team>();
            foreach (var team in listOfTeams)
            {
                ListWithOutAdded.Add(team);
            }
             TeamsAdded = player.Teams;
            foreach (var tm in listOfTeams)
            {
                foreach (var tmA in TeamsAdded)
                {
                    if (tm.Id == tmA.Id)
                    {
                        ListWithOutAdded.Remove(tm);
                    }
                }
            }
            
        }

    }
}