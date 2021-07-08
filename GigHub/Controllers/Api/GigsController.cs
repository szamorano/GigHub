using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _db;

        public GigsController()
        {
            _db = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _db.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            //if (gig.IsCanceled)
            //    return NotFound();

            gig.IsCanceled = true;

            //var notification = new Notification
            //{
            //    DateTime = DateTime.Now,
            //    Gig = gig,
            //    Type = NotificationType.GigCanceled
            //};

            //var attendees = _db.Attendances
            //    .Where(a => a.GigId == gig.Id)
            //    .Select(a => a.Attendee)
            //    .ToList();

            //foreach (var attendee in attendees)
            //{
            //    var userNotification = new UserNotification
            //    {
            //        User = attendee,
            //        Notification = notification
            //    };
            //    _db.UserNotifications.Add(userNotification);
            //}

            _db.SaveChanges();

            return Ok();
        }
    }
}
