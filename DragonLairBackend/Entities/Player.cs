using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entities
{
    public class Player
    {
    
        [Required]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        public virtual List<Team> Teams { get; set; }
    }
}