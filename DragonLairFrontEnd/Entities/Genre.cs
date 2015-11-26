using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public virtual List<Game> Games { get; set; }
    }
}