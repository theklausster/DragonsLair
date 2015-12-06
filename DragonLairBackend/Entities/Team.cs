using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entities
{
    public class Team
    {
        public Team()
        {
        }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a Number")]
        public int Draw { get; set; }
        [Required(ErrorMessage = "Enter a Number")]
        public int Loss { get; set; }
        [Required(ErrorMessage = "Enter a Number")]
        public int Win { get; set; }
        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "DtoTeams")]
        public virtual List<Group> Groups { get; set; }
        [JsonProperty(PropertyName = "DtoPlayers")]
        public virtual List<Player> Players { get; set; }

    }
}