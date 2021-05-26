using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models.Venue
{
    public class VenueDetail
    {
        [Display(Name = "Venue ID")]
        public int VenueID { get; set; }

        [Display(Name = "Name")]
        public string VenueName { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Opened")]
        public int YearOpened { get; set; }

        public int Capacity { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        //[Display(Name = "Visited")]
        //public bool HasVisited { get; set; }
    }
}
