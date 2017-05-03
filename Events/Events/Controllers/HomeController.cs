using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var eventFromCache = HttpRuntime.Cache["event"];
            if (eventFromCache == null)
            {
                var barEvent = new ApplicationDbContext().Events
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
                eventFromCache = HttpRuntime.Cache["event"];
            }
            return View(eventFromCache);
        }




    }
}