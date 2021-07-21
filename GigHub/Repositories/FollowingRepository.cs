using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _db;

        public FollowingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool GetFollowing(Gig gig, string userId)
        {
            return _db.Followings.Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
        }
    }
}