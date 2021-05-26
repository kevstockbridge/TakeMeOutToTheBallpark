using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Models.Event
{
    public class UserEvent
    {
        public int ProfileID { get; set; }
        public string VenueName { get; set; }
        public string TeamName { get; set; }
        public string Comments { get; set; }
    }
}
