using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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
            var gig = _db.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _db.SaveChanges();

            return Ok();
        }
    }
}
