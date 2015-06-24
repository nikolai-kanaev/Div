using Pub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Pub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();
                using (var db = new ApplicationDbContext())
                {
                    ViewBag.IsInQueue = db.Users.First(u => u.Id == currentUserId).IsInLine;

                }
            }
            else { ViewBag.IsInQueue = false; }
            return View();
        }

        [Authorize]
        public ActionResult SeeRandomTen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SeeRandomTenData()
        {
            var currentUserId = User.Identity.GetUserId();
            List<ApplicationUser> usersInQueue;
            List<PublicProfile> publicProfiles = new List<PublicProfile>();
            using (var db = new ApplicationDbContext())
            {
                var currentUser = db.Users.First(u => u.Id == currentUserId);
                currentUser.CooldownEnds = DateTime.Now.AddMinutes(1);
                db.Entry(currentUser).Property(u => u.CooldownEnds).IsModified = true;
                db.SaveChanges();

                usersInQueue = db.Users.Where(u => u.Id != currentUserId && u.IsInLine).Take(6).ToList();
            }
            using (var db = new PubContext())
            {
                foreach (var user in usersInQueue)
                {
                    publicProfiles.Add(db.PublicProfiles.FirstOrDefault(p => p.UserId == user.Id));
                }
            }
            return Json(publicProfiles);
        }

        [HttpPost]
        [Authorize]
        public ActionResult GetInLine()
        {
            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var currentUser = db.Users.First(u => u.Id == currentUserId);
                currentUser.IsInLine = !currentUser.IsInLine;
                db.Entry(currentUser).Property(u => u.IsInLine).IsModified = true;
                db.SaveChanges();

                return Json(true);
            }
        }

        //[HttpPost]
        //[Authorize]
        //public ActionResult SeeRandomTen()
        //{
        //    var currentUserId = User.Identity.GetUserId();
        //    using (var db = new ApplicationDbContext())
        //    {
        //        var currentUser = db.Users.First(u => u.Id == currentUserId);
        //        currentUser.CooldownEnds = DateTime.Now.AddMinutes(1);
        //        db.Entry(currentUser).Property(u => u.CooldownEnds).IsModified = true;
        //        db.SaveChanges();

        //        return Json(true);
        //    }
        //}

        [HttpPost]
        [Authorize]
        public ActionResult GetRemainingCooldown()
        {
            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var cooldownEnds = db.Users.First(u => u.Id == currentUserId).CooldownEnds;
                var differenceInTime = cooldownEnds - DateTime.Now;

                if (cooldownEnds < DateTime.Now)
                {
                    return Json(false);
                }
                var returnTimeSpan = differenceInTime.ToString(@"mm\:ss");
                return Json(returnTimeSpan);
            }
        }

        [HttpPost]
        public ActionResult GetPeopleInQueue()
        {
            using (var db = new ApplicationDbContext())
            {
                var usersInQueue = db.Users.Count(u => u.IsInLine);
                return Json(usersInQueue);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult IsUserOnCooldown()
        {
            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var cooldownEnds = db.Users.First(u => u.Id == currentUserId).CooldownEnds;
                if (cooldownEnds > DateTime.Now)
                {
                    return Json(true);
                }

                return Json(false);

            }
        }
    }
}