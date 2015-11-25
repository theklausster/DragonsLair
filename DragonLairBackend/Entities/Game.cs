using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Entities
{
    public class Game
    {
        public Game()
        {
        }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter a Name")]
        public string Name { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual List<Tournament> Tournaments { get; set; }
    }
}