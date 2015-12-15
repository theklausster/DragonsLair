using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Team Winner { get; set; }
        [JsonProperty(PropertyName = "DtoTournament")]
        public virtual Tournament Tournament { get; set; }

    }
}
