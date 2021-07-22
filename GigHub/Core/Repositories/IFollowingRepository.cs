using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        bool GetFollowing(Gig gig, string userId);
    }
}