using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _db;

        public AttendanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool GetAttendance(int gigId, string userId)
        {
            return _db.Attendances.Any(a => a.GigId == gigId && a.AttendeeId == userId);
        }


        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _db.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

    }
}