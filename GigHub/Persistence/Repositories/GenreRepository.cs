using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Genre> GetGenres()
        {
            return _db.Genres.ToList();
        }

    }
}