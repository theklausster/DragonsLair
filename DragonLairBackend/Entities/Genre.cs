using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entities
{
    public class Genre
    {
        public Genre()
        {
        }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "DtoGames")]
        public virtual List<Game> Games { get; set; }
    }
}