using Pub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Pub.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult EditPublicProfile()
        {
            var currentUserId = User.Identity.GetUserId();

            using (var db = new PubContext())
            {
                var userProfile = db.PublicProfiles.FirstOrDefault(p => p.UserId == currentUserId);
                if (userProfile == null)
                {
                    userProfile = new PublicProfile() { UserId = currentUserId };
                    db.PublicProfiles.Add(userProfile);
                    db.SaveChanges();
                }
                return View(userProfile);
            }
        }

        [HttpPost]
        public ActionResult EditPublicProfile(PublicProfile publicProfile)
        {
            var currentUserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                using (var db = new PubContext())
                {
                    db.Entry(publicProfile).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(publicProfile);
        }
    }
}