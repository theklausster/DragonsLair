using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Entities
{
    public class Player
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required]
        [JsonProperty(PropertyName = "DtoTeams")]
        public List<Team> Teams { get; set; }
    }
}