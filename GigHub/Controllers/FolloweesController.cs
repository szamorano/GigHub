using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _db;

        public FolloweesController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artists = _db.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(artists);
        }
    }
}
