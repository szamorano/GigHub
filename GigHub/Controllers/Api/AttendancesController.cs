﻿using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;

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

            if (_db.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _db.Attendances.Add(attendance);
            _db.SaveChanges();

            return Ok();

        }








        //[HttpPost]
            //public IHttpActionResult Attend([FromBody] int gigId)
            //{
            //    var userId = User.Identity.GetUserId();

            //    if (_db.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId))
            //        return BadRequest("The attendance already exists.");

            //    var attendance = new Attendance
            //    {
            //        GigId = gigId,
            //        AttendeeId = userId
            //    };

            //    _db.Attendances.Add(attendance);
            //    _db.SaveChanges();

            //    return Ok();
            //}

            [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _db.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);

            if (attendance == null)
                return NotFound();

            _db.Attendances.Remove(attendance);
            _db.SaveChanges();

            return Ok(id);
        }
    }
}
