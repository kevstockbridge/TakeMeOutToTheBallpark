using Ballpark.Models;
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
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()     //displays all the profiles for the current user.
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileService(userID);
            var model = service.GetProfiles();

            List<string> ListOfProfiles = new List<string>();
            foreach (var prof in model)
            {
                ListOfProfiles.Add(prof.FullName);
            }
            ViewBag.VenueCount = ListOfProfiles.Count();

            return View(model);
        }

        // GET: Profile
        //Profile/Create
        public ActionResult Create()        //making a request to get the Create View
        {
            return View();
        }

        // POST: Profile
        //Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateProfileService();

            if (service.CreateProfile(model))
            {
                TempData["SaveResult"] = "Your profile was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Profile could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateProfileService();
            var model = svc.GetProfileByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateProfileService();
            var detail = service.GetProfileByID(id);
            var model =
                new ProfileEdit
                {
                    ProfileID = detail.ProfileID,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    FavTeam = detail.FavTeam
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProfileEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ProfileID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateProfileService();

            if (service.UpdateProfile(model))
            {
                TempData["SaveResult"] = "Your profile was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your profile could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateProfileService();
            var model = svc.GetProfileByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateProfileService();

            service.DeleteProfile(id);

            TempData["SaveResult"] = "Your profile was deleted.";

            return RedirectToAction("Index");
        }

        private ProfileService CreateProfileService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileService(userID);
            return service;
        }
    }
}