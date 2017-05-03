namespace Events.Migrations
{
    using Events.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Events.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Events.Models.ApplicationDbContext context)
        {
            var ownerRole = "owner";
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            if (!context.Roles.Any(a => a.Name == ownerRole))
            {
                var role = new IdentityRole { Name = ownerRole };
                manager.Create(role);
            }
            var visitorRole = "visitor";
            if (!context.Roles.Any(a => a.Name == visitorRole))
            {
                var role = new IdentityRole { Name = visitorRole };
                manager.Create(role);
            }
            var ownerEmail = "owner@localebar.com";
            var defaultPassword = "Password1!";
            if (!context.Users.Any(u => u.UserName == ownerEmail))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = ownerEmail };

                userManager.Create(user, defaultPassword);
                userManager.AddToRole(user.Id, ownerRole);
            }

            var genres = new List<GenreModel>
            {
             new GenreModel { Name = "Live Music" },
             new GenreModel { Name = "Wine Tasting" },
             new GenreModel { Name = "Beer Tasting" },
            };
            genres.ForEach(genre =>
            {
                context.Genres.AddOrUpdate(g => g.Name, genre);
                context.SaveChanges();
            });

            var venues = new List<VenueModel>
            {
            new VenueModel { Name = "The Blueberry Patch" },
            new VenueModel { Name = "Otherside Winery" },
            new VenueModel { Name = "Sunset Beach Bar" },
            };
            venues.ForEach(venue =>
            {
                context.Venues.AddOrUpdate(v => v.Name, venue);
                context.SaveChanges();
            });

            var events = new List<EventModel>
            {
                new EventModel {
                    Title = "It's Time to Get FUNKY",
                    Description="Come see all of our wonderful local performers!",
                    StartTime =new DateTime(2017, 07, 04, 13, 00, 00),
                    EndTime =new DateTime(2017, 07, 04, 22, 00, 00),
                    Genre =new GenreModel{Name="Live Music"},
                    Venue=new VenueModel{Name="The Blueberry Patch"},
                },
                new EventModel {
                    Title = "Wine Down",
                    Description="Complain less, Wine More with the Locale Bar.",
                    StartTime =new DateTime(2017, 07, 12, 13, 00, 00),
                    EndTime =new DateTime(2017, 07, 12, 18, 00, 00),
                    Genre =new GenreModel{Name="Wine Tasting"},
                    Venue=new VenueModel{Name="Otherside Winery"},
                },
                new EventModel {
                    Title = "Fun and Flights",
                    Description="Sunset flights on the beach with Locale Bar.",
                    StartTime =new DateTime(2017, 07, 19, 11, 00, 00),
                    EndTime =new DateTime(2017, 07, 19, 20, 00, 00),
                    Genre =new GenreModel{Name="Beer Tasting"},
                    Venue=new VenueModel{Name="Sunset Beach Bar"},
                },
            };
            events = events.Select(s => new EventModel(s) {GenreId = genres.Select(g=>g.Id).First()}).ToList();
            events = events.Select(s => new EventModel(s) { VenueId = venues.Select(v=>v.Id).First() }).ToList();
            events.ForEach(barEvent =>
            {
                context.Events.AddOrUpdate(e => e.Title, barEvent);
            });
            context.SaveChanges();
        }
    }
}
