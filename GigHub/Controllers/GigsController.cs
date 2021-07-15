using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GigsController()
        {
            _db = new ApplicationDbContext();
        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _db.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _db.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _db.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _db.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _db.Genres.ToList(),
                Date = gig.DateTime.ToString("MMM d yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _db.Genres.ToList();
                return View("GigForm", viewModel);
            }
                

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _db.Gigs.Add(gig);
            _db.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _db.Genres.ToList();
                return View("GigForm", viewModel);
            }


            var userId = User.Identity.GetUserId();
            var gig = _db.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single((g => g.Id == viewModel.Id && g.ArtistId == userId));

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _db.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        public ActionResult Details(int id)
        {
            var gig = _db.Gigs.Include(g => g.Artist).Include(g => g.Genre).SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel {Gig = gig};

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _db.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _db.Followings.Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }
    }
}