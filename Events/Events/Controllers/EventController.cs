using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Events.Models;

namespace Events.Controllers
{
    [Authorize(Roles = "owner")]
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event
        public ActionResult Index()
        {
            var barEvent = db.Events.OrderBy(o => o.StartTime).ToList();
            return View(barEvent);
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,StartTime,EndTime,VenueId,GenreId")] EventModel eventModel)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(eventModel);
                db.SaveChanges();

                HttpRuntime.Cache.Remove("event");

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
                return RedirectToAction("Index", "Home");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", eventModel.GenreId);
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", eventModel.VenueId);
            return View(eventModel);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", eventModel.GenreId);
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", eventModel.VenueId);
            return View(eventModel);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,StartTime,EndTime,VenueId,GenreId")] EventModel eventModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventModel).State = EntityState.Modified;
                db.SaveChanges();

                HttpRuntime.Cache.Remove("event");

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
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", eventModel.GenreId);
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", eventModel.VenueId);
            return View(eventModel);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventModel eventModel = db.Events.Find(id);
            db.Events.Remove(eventModel);
            db.SaveChanges();

            HttpRuntime.Cache.Remove("event");

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
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
