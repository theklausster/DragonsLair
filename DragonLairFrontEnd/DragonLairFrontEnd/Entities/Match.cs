using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entities
{
    public class Match
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Round name")]
        public string Round { get; set; }
        [Display(Name = "Winner is: ")]
        public Team Winner { get; set; }
        [JsonProperty(PropertyName = "DtoTeams")]
        public virtual List<Team> Teams { get; set; }
        [JsonProperty(PropertyName = "DtoTournament")]
        public virtual Tournament Tournament { get; set; }
        [JsonProperty(PropertyName = "DtoGroup")]
        public virtual Group Group { get; set; }
    }
}