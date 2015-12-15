using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Match
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Round name")]
        public string Round { get; set; }
        public Team Winner { get; set; }
        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }
        [JsonProperty(PropertyName = "DtoTournament")]
        public virtual Tournament Tournament { get; set; }

    }
}
