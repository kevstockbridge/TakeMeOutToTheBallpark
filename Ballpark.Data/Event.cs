using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballpark.Data
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public DateTimeOffset DateOfGame { get; set; }

        [Required]
        public Guid OwnerID { get; set; }

        public string Result { get; set; }

        public string Comments { get; set; }

        [ForeignKey(nameof(Profile))]
        public int ProfileID { get; set; }
        public virtual Profile Profile { get; set; }


        [ForeignKey(nameof(HomeTeam))]
        public int HomeID { get; set; }
        public virtual Team HomeTeam { get; set; }


        [ForeignKey(nameof(AwayTeam))]
        public int AwayID { get; set; }
        public virtual Team AwayTeam { get; set; }
    }
}
