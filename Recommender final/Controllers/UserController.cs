using Recommender.Models;
using Recommender_final.DAL;
using Recommender_final.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recommender_final.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index(string userName)
        {
            using(RecommenderContext db = new RecommenderContext())
            {
                var users = db.UserToUser.Where(u => u.User1.Name == userName).Select(x => x.User2).ToList();
                return View("Index", users);
            }
        }
        public ActionResult RecommendUsers(string userName)
        {
            User user;
            using (RecommenderContext db = new RecommenderContext())
            {
                user = db.Users.Where(u => u.Name == userName).FirstOrDefault();
            }

            Slope slope = new Slope();
            var recommendedUsers = slope.GetRecommendations(user);
            foreach(var u in recommendedUsers)
            {
                using(RecommenderContext db = new RecommenderContext())
                {
                    db.UserToUser.Add(new UserToUser()
                    { 
                        User1 = user,
                        User2 = u.Key
                    });
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { userName = userName });
        }
    }
}