using System.ComponentModel.DataAnnotations;

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

        public virtual Genre genre { get; set; }
    }
}