using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entities
{
    public class Match
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Round name")]
        public string Round { get; set; }

      
        public virtual Team Winner { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

        [JsonProperty(PropertyName = "DtoTournament")]
        public virtual Tournament Tournament { get; set; }

    }
}