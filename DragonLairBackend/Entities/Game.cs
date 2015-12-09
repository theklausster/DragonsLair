using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Newtonsoft.Json;

namespace Entities
{
    public class Game
    {
      [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter a Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "DTOGenre")]
        public virtual Genre Genre { get; set; }
        [JsonProperty(PropertyName = "DTOTournaments")]
        public virtual List<Tournament> Tournaments { get; set; }
    }
}