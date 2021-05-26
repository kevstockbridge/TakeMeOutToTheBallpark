using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models.Venue
{
    public class VenueEdit
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int YearOpened { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}
