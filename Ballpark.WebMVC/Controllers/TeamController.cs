using Ballpark.Models.Team;
using Ballpark.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ballpark.WebMVC.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            var service = new TeamService();
            var model = service.GetTeams();
            return View(model);
        }

        [Authorize (Roles = "Admin")]
        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTeamService();

            if (service.CreateTeam(model))
            {
                TempData["SaveResult"] = "Your team was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Team could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateTeamService();
            var model = svc.GetTeamByID(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit (int id)
        {
            var service = CreateTeamService();
            var detail = service.GetTeamByID(id);
            var model =
                new TeamEdit
                {
                    TeamID = detail.TeamID,
                    TeamName = detail.TeamName,
                    VenueName = detail.VenueName
                };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TeamID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateTeamService();

            if (service.UpdateTeam(model))
            {
                TempData["SaveResult"] = "Your team was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your team could not be updated.");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult Delete (int id)
        {
            var svc = CreateTeamService();
            var model = svc.GetTeamByID(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost (int id)
        {
            var service = CreateTeamService();

            service.DeleteTeam(id);

            TempData["SaveResult"] = "Your team was deleted.";

            return RedirectToAction("Index");
        }

        private static TeamService CreateTeamService()
        {
            return new TeamService();
        }
    }
}