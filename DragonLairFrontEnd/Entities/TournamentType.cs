using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TournamentType
    {
        public TournamentType()
        {
        }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Type")]
        public string Type { get; set; }

        public virtual List<Tournament> Tournaments { get; set; }
    }
}