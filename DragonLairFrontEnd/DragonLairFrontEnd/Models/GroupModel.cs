using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using ServiceGateway.Http;

namespace DragonLairFrontEnd.Models
{
    public class GroupModel
    {
        public List<Group> Groups { get; set; } 
        public List<Team> Teams { get; set; } 
        public List<Tournament> Tournaments { get; set; } 
        public Group Group { get; set; }
        public Tournament Tournament { get; set; }

        public GroupModel()
        {

            
        }

      }
}