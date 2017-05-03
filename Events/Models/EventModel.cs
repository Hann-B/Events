using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EventModel
    {
        public EventModel() { }

        public EventModel(EventModel s)
        {
            this.Title = s.Title;
            this.StartTime = s.StartTime;
            this.EndTime = s.EndTime;
            this.GenreId = s.GenreId;
            this.Id = s.Id;
            this.VenueId = s.VenueId;
            this.Description = s.Description;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString="{0:}MM/dd h:mm")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:}MM/dd h:mm")]
        public DateTime EndTime { get; set; }

        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public VenueModel Venue { get; set; }

        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public GenreModel Genre { get; set; }
    }
}