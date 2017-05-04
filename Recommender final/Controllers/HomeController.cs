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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        [HttpGet]
        public ActionResult FetchUsers()
        {
            Service.ApiManager manager = new Service.ApiManager();
            var users2 = manager.GetFriends("col403");

            var users = manager.GetUsers("col403");
            using (RecommenderContext db = new RecommenderContext())
            {
                foreach(var user in users)
                {
                    db.Users.Add(new Recommender.Models.User() { Name = user.name});
                }
                db.SaveChanges();
            }
            ViewBag.Message = "success: users";
            return View("Index");
        }

        [HttpGet]
        public ActionResult FetchArtists()
        {
            Service.ApiManager manager = new Service.ApiManager();
            using (RecommenderContext db = new RecommenderContext())
            {
                var users = db.Users.ToList();
                foreach (var user in users)
                {
                    List<Service.Track> tracks = new List<Service.Track>();
                    manager.GetTracks(user.Name, 1, tracks);
                    foreach (var track in tracks)
                    {
                        var artist = new Artist() { Name = track.artist.name };
                        db.Artists.Add(artist);
                        db.Artists.AddIfNotExists(artist, x => x.Name == artist.Name);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }

            }
            ViewBag.Message = "success: artists";
            return View("Index");
        }

        [HttpGet]
        public ActionResult FetchTags()
        {
            Service.ApiManager manager = new Service.ApiManager();
            using (RecommenderContext db = new RecommenderContext())
            {
                var atists = db.Artists.ToList();
                foreach (var artist in atists)
                {
                    List<Service.Track> tracks = new List<Service.Track>();
                    var tags = manager.GetTags(artist.Name);
                    foreach (var tag in tags)
                    {
                        var newTag = new Tag() { Name = tag };
                        db.Tags.AddIfNotExists(newTag, x => x.Name == tag);
                        db.SaveChanges();
                        var tagFromDb = db.Tags.Where(t => t.Name == tag).FirstOrDefault();
                        Artist_Tag a_t = new Artist_Tag() { Artist = artist, Tag = tagFromDb };
                        db.Artist_Tags.Add(a_t);
                        db.SaveChanges();
                    }
                    //db.SaveChanges();
                }

            }
            ViewBag.Message = "success: artists";
            return View("Index");
        }

        [HttpGet]
        public ActionResult FetchTracks()
        {
            Service.ApiManager manager = new Service.ApiManager();
            List<User> users;
            using (RecommenderContext db = new RecommenderContext())
            {
                users = db.Users.Where(u => u.ID >19).ToList();

            }
            foreach (var user in users)
                {
                    List<Service.Track> tracks = new List<Service.Track>();
                    manager.GetTracks(user.Name, 1, tracks);
                    using (RecommenderContext db = new RecommenderContext())
                    {
                        foreach (var track in tracks)
                        {
                            var artist = db.Artists.Where(x => x.Name == track.artist.name).FirstOrDefault();
                            Track newTrack = new Track() { Name = track.name, Artist = artist };

                            db.Tracks.AddIfNotExists(newTrack, x => x.Name == track.name);
                            db.SaveChanges();
                            var trackFromDb = db.Tracks.Where(x => x.Name == newTrack.Name).FirstOrDefault();
                            db.Scrobbles.Add(new Scrobble() { User = user, Count = Int32.Parse(track.playcount), Track = trackFromDb });
                            db.SaveChanges();
                        }
                    }
                }

            ViewBag.Message = "success: tracks";
            return View("Index");
        }

    }
}