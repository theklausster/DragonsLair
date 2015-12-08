using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Tournament
    {
        public Tournament()
        {
        }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [JsonProperty(PropertyName = "DTOTournamentType")]
        public virtual TournamentType TournamentType { get; set; }
        [JsonProperty(PropertyName = "DtoGame")]
        public virtual Game Game { get; set; }
        [JsonProperty(PropertyName = "DtoGroups ")]
        public virtual List<Group> Groups { get; set; }


    }
}