using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Entities
{
    public class Team
    {
        public Team()
        {
        }
        [Required]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a Number")]
        public int Draw { get; set; }
        [Required(ErrorMessage = "Enter a Number")]
        public int Loss { get; set; }
        [Required(ErrorMessage = "Enter a Number")]
        public int Win { get; set; }
        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "DtoGroups")]
        public  List<Group> Groups { get; set; }
        [JsonProperty(PropertyName = "DtoPlayers")]
        public  List<Player> Players { get; set; }

    }
}