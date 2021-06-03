using System.Linq;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Dtos;

namespace GigHub.Controllers
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _db;

        public AttendancesController()
        {
            _db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_db.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GidId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                GigId = dto.GidId,
                AttendeeId = userId
            };

            _db.Attendances.Add(attendance);
            _db.SaveChanges();

            return Ok();
        }
    }
}
