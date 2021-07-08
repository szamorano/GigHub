﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _db;

        public FollowingsController()
        {
            _db = new ApplicationDbContext();
        }

        //[HttpPost]
        //public IHttpActionResult Follow(FollowingDto dto)
        //{
        //    var userId = User.Identity.GetUserId();

        //    if (_db.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
        //        return BadRequest("Following already exists.");

        //    var following = new Following
        //    {
        //        FollowerId = userId,
        //        FolloweeId = dto.FolloweeId
        //    };
        //    _db.Followings.Add(following);
        //    _db.SaveChanges();

        //    return Ok();





        [HttpPost]
        public IHttpActionResult Follow([FromBody] string followeeId)
        {
            var userId = User.Identity.GetUserId();

            if (_db.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == followeeId))
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followeeId
            };
            _db.Followings.Add(following);
            _db.SaveChanges();

            return Ok();
        }
    }
}