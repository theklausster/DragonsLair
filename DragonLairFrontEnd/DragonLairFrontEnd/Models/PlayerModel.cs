using System.Collections.Generic;
using Entities;

namespace DragonLairFrontEnd.Models
{
    public class PlayerModel
    {
        public Player Player { get; set; }
        public List<Team> DtoTeams { get; set; }

    }
}