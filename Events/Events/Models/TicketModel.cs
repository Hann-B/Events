using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public DateTime TimePurchased { get; set; } = DateTime.Now;
        public bool Fulfilled { get; set; } = false;
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<EventModel> Events { get; set; } = new HashSet<EventModel>();

        [DisplayFormat(DataFormatString = "{0:C}")]
        [NotMapped]
        public double TotalPrice
        {
            get
            {
                return Events.Sum(s => s.Price);
            }
        }
    }
}