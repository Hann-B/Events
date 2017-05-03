using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class VenueModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<EventModel> Events { get; set; } = new HashSet<EventModel>();
    }
}