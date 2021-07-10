using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _db;

        public NotificationsController()
        {
            _db = new ApplicationDbContext();
        }

        public IEnumerable<Notification> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _db.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications;
        }
    }
}
