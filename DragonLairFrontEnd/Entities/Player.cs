using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Player
    {
        public Player()
        {
        }
        
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        //public virtual List<Team> Teams { get; set; }
    }
}