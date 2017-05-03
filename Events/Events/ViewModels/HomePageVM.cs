using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.ViewModels
{
    public class HomePageVM
    {
        public virtual ICollection<EventModel> Events { get; set; }

        public TicketModel ShoppingCart { get; set; }
    }
}