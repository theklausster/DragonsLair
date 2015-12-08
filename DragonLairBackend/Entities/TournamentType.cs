using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TournamentType
    {
        public TournamentType()
        {
        }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "DtoTournament")]
        public virtual List<Tournament> Tournaments { get; set; }
    }
}