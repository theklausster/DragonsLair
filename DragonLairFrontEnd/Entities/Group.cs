using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Entities
{
    public class Group
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "DtoTournament")]
        public Tournament Tournament { get; set; }
        [JsonProperty(PropertyName = "DtoTeams")]
        public List<Team> Teams { get; set; }
    }
}