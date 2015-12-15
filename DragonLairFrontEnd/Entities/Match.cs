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

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        [JsonProperty(PropertyName = "DtoTournament")]
        public virtual Tournament Tournament { get; set; }

    }
}