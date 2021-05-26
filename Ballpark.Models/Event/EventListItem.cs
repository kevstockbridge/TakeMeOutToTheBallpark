using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models.Event
{
    public class EventListItem
    {
        [Display(Name = "Date")]
        public DateTimeOffset DateOfGame { get; set; }

        [Display(Name = "Event ID Number")]
        public int EventID { get; set; }

        [Display(Name = "Venue")]
        public string VenueName { get; set; }

        [Display(Name = "Home Team")]
        public string HomeTeam { get; set; }

        [Display(Name = "Away Team")]
        public string AwayTeam { get; set; }
        public string Result { get; set; }
    }
}
