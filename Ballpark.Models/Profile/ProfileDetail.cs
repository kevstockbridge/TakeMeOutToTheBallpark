using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models
{
    public class ProfileDetail
    {
        [Display(Name = "Profile ID Number")]
        public int ProfileID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Display(Name = "Favorite Team")]
        public string FavTeam { get; set; }

        [Display(Name = "Joined")]
        public DateTimeOffset CreatedUtc { get; set; }

        //[Display(Name = "Stadiums Visited")]
        //public int? StadiumsVisited { get; set; }
    }
}
