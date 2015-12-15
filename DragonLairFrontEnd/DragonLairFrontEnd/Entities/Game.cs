using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Entities
{
    [DataContract]
    public class Game
    {
        public Game()
        {
        }
        [Required]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Please Enter a Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "DtoGenre")]
        public Genre Genre { get; set; }
        [JsonProperty(PropertyName = "DtoTournaments")]
        public List<Tournament> Tournaments { get; set; }
    }
}