using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models
{
    public class ProfileCreate
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage ="There are too many characters in this field.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "There are too many characters in this field.")]
        public string LastName { get; set; }

        [Display(Name = "Favorite Team")]
        public string FavTeam { get; set; }
    }
}
