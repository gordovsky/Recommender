using Recommender.Models;
using Recommender_final.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recommender_final.Lib
{
    public class Slope
    {
        public Slope()
        {

        }

        public Dictionary<User, double> GetRecommendations(User user)
        {
            using(RecommenderContext db = new RecommenderContext())
            {
                Dictionary<User, double> similarUsers = new Dictionary<User, double>();
                var users = db.Users.Where(s => s.Scrobbles.Count!=0).ToList();

                var artstsCount = db.Scrobbles.Where(u => u.UserId == user.ID).Select(sc => sc.Track.Artist).Count();
                var topArtists = db.Scrobbles.Where(u => u.UserId == user.ID).OrderByDescending(s => s.Count).Take(20 + artstsCount/30).Select(a => a.Track.Artist).ToList();



                foreach(var u in users)
                {
                    var aCount = db.Scrobbles.Where(ac => ac.ID == u.ID).Select(sc => sc.Track.Artist).Count();
                    var topA = db.Scrobbles.Where(s => s.ID == u.ID).OrderByDescending(s => s.Count).Take(20 + artstsCount / 30).Select(a => a.Track.Artist).ToList();

                    var intersectedCount = topArtists.Intersect(topA).Count();
                    similarUsers.Add(u, intersectedCount);
                    similarUsers = similarUsers.OrderByDescending(t => t.Value).Take(10).Select(t => new { t.Key, t.Value }).ToDictionary(t => t.Key, t => t.Value);
                }

                return similarUsers;
            }
        }
    }
}