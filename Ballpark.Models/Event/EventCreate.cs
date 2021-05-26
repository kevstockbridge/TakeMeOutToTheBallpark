using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models.Event
{
    public class EventCreate
    {
        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTimeOffset DateOfGame { get; set; }

        [Display(Name = "Profile")]
        public int ProfileID { get; set; }

        [Display(Name = "Venue ID Number")]
        public int VenueID { get; set; }
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Display(Name = "Home Team")]
        public int HomeTeamID { get; set; }

        [Display(Name = "Away Team")]
        public int AwayTeamID { get; set; }

        public string Result { get; set; }

        public string Comments { get; set; }
    }
}
