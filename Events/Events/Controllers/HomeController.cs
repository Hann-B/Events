using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using System.Data.Entity;
using Events.ViewModels;
using System.Data.Entity;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var eventFromCache = HttpRuntime.Cache["event"] as ICollection<EventModel>;
            if (eventFromCache == null)
            {
                var barEvent = new ApplicationDbContext().Events
                    .Include(i => i.Genre)
                    .Include(i => i.Venue)
                    .OrderBy(o => o.StartTime)
                    .ToList();
                HttpRuntime.Cache.Add(
                    "event",
                    barEvent,
                    null,
                    DateTime.Now.AddMonths(1),
                    new TimeSpan(),
                    System.Web.Caching.CacheItemPriority.High,
                    null);
                eventFromCache = HttpRuntime.Cache["event"] as ICollection<EventModel>;
            }

            var vm = new HomePageVM
            {
                Events= eventFromCache,
                ShoppingCart= Session["cart"] as TicketModel??new TicketModel()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult ShoppingCart(int id)
        {
            var cart = Session["cart"] as TicketModel;
            if (cart == null)
            {
                cart = new TicketModel()
                {
                    Fulfilled = false,
                    TimePurchased = DateTime.Now
                };
            }
            var eventToAdd = new ApplicationDbContext().Events
                .Include(i => i.Genre)
                .Include(i => i.Venue)
                .FirstOrDefault(f => f.Id == id);
            cart.Events.Add(eventToAdd);
            Session["cart"] = cart;
            return PartialView("_shoppingCart", cart);
        }

        public ActionResult Checkout()
        {
            var vm = Session["cart"] as TicketModel;
            return View(vm);
        }

        [HttpDelete]
        public ActionResult RemoveFromCart(string id)
        {
            var cart = Session["cart"] as TicketModel;
            cart.Events = cart.Events.Where(w => w.TrackerId != Guid.Parse(id)).ToList();
            return PartialView("_checkoutDisplayCart", cart);
        }

    }
}