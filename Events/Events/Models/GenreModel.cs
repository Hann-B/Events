using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class GenreModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<EventModel> events { get; set; }
    }
}