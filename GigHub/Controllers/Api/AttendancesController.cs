using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
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
