using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Data
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        //[Required]
        //public Guid OwnerID { get; set; }

        [Required]
        public string VenueName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int YearOpened { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public bool IsActive { get; set; }

        //public bool HasVisited { get; set; }

    }
}
