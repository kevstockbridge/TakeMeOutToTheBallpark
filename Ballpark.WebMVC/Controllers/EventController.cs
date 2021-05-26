using Ballpark.Data;
using Ballpark.Models.Event;
using Ballpark.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ballpark.WebMVC.Controllers
{
    [Authorize]
    public class EventController : Controller
    {

        private ApplicationDbContext _database = new ApplicationDbContext();

        // GET: Event
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userID);
            var model = service.GetEvents();

            List<string> ListOfVenueNames = new List<string>();
            foreach (var venueName in model)
            {
                ListOfVenueNames.Add(venueName.VenueName);
            }
            HashSet<string> HashSetOfVenueNames = new HashSet<string>(ListOfVenueNames);
            ViewBag.VenueCount = HashSetOfVenueNames.Count();
            return View(model);
        }

        public ActionResult GetVisitedVenues()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userID);
            var model = service.GetVenueHashSet();
            List<string> modelList = model.ToList();
            ViewBag.VisitedVenues = modelList;
            ViewBag.VenueCount = model.Count();
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(_database.Profiles.ToArray().OrderBy(f => f.FirstName), "ProfileID", "FirstName");
            ViewBag.HomeTeamID = new SelectList(_database.Teams.ToArray().OrderBy(t => t.TeamName), "TeamID", "TeamName");
            ViewBag.AwayTeamID = new SelectList(_database.Teams.ToArray().OrderBy(t => t.TeamName), "TeamID", "TeamName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateEventService();

            if (service.CreateEvent(model))
            {
                TempData["SaveResult"] = "Your event was successfully added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Event could not be added.");


            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateEventService();
            var model = svc.GetEventByID(id);

            return View(model);
        }

        public ActionResult DetailsByVenue(string venue)
        {
            var svc = CreateEventService();
            var model = svc.GetEventByVenueName(venue);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateEventService();
            var detail = service.GetEventByID(id);
            var model =
                new EventEdit
                {
                    EventID = detail.EventID,
                    DateOfGame = detail.DateOfGame,
                    HomeTeamID = detail.HomeID,
                    AwayTeamID = detail.AwayID,
                    Result = detail.Result,
                    Comments = detail.Comments
                };

            ViewBag.HomeTeamID = new SelectList(_database.Teams.ToArray().OrderBy(t => t.TeamName), "TeamID", "TeamName");
            ViewBag.AwayTeamID = new SelectList(_database.Teams.ToArray().OrderBy(t => t.TeamName), "TeamID", "TeamName");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, EventEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.EventID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateEventService();

            if (service.UpdateEvent(model))
            {
                TempData["SaveResult"] = "Your event was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your event could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateEventService();
            var model = svc.GetEventByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateEventService();

            service.DeleteEvent(id);

            TempData["SaveResult"] = "Your event was deleted.";

            return RedirectToAction("Index");
        }

        private EventService CreateEventService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userID);
            return service;
        }
    }
}