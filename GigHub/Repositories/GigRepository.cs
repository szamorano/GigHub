using GigHub.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private readonly ApplicationDbContext _db;

        public GigRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _db.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

    }
}