using Events.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Events.ViewModels
{
    public class TicketSalesVM
    {
        [ForeignKey("CustomerId")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<EventModel> Events { get; set; } = new HashSet<EventModel>();
    }
}