using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        bool GetFollowing(Gig gig, string userId);
    }
}