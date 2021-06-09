using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Dtos
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _db;

        public FollowingsController()
        {
            _db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_db.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _db.Followings.Add(following);
            _db.SaveChanges();

            return Ok();
        }
    }
}
