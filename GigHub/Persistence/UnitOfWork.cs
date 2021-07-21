using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; set; }

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