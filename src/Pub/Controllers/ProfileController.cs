using Pub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.IO;

namespace Pub.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult EditPublicProfile()
        {
            var currentUserId = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var userProfileImageUri = db.Users.FirstOrDefault(u => u.Id == currentUserId).ProfileImageUri;
                ViewBag.ProfileImageUri = userProfileImageUri;
            }
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

        public ActionResult UploadProfileImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Profile"), fileName);


                using (var db = new ApplicationDbContext())
                {
                    var userId = User.Identity.GetUserId();
                    var currentUser = db.Users.FirstOrDefault(u => u.Id == userId);
                    if (currentUser.ProfileImageUri != null)
                    {
                        var success = RemoveCurrentImage(currentUser.ProfileImageUri);
                    }
                    currentUser.ProfileImageUri = fileName;
                    db.Entry(currentUser).State = EntityState.Modified;
                    db.SaveChanges();
                }

                // file is uploaded
                file.SaveAs(path);

            }
            // after successfully uploading redirect the user
            return RedirectToAction("EditPublicProfile");
        }

        private bool RemoveCurrentImage(string imageUri)
        {
            try
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/Profile"), imageUri);
                System.IO.File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}