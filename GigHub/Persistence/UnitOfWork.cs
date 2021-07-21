using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public GigRepository Gigs { get; private set; }
        public AttendanceRepository Attendances { get; private set; }
        public FollowingRepository Followings { get; private set; }
        public GenreRepository Genres { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Gigs = new GigRepository(db);
            Attendances = new AttendanceRepository(db);
            Followings = new FollowingRepository(db);
            Genres = new GenreRepository(db);
        }

        public void Complete()
        {
            _db.SaveChanges();
        }
    }
}