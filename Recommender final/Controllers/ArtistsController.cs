using Recommender_final.DAL;
using Recommender_final.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recommender_final.Controllers
{
    public class ArtistsController : Controller
    {
        // GET: Artists
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult MaintainArtist(string artistName)
        {
            using (RecommenderContext db = new RecommenderContext())
            {
                var artist = db.Artists.Where(a => a.Name == artistName).FirstOrDefault();
                NBC nbc = new NBC();
                var similarArtists = nbc.GetSimilarArtists(artist);
                foreach(var sa in similarArtists)
                {
                    db.ArtistToArtist.Add(new Recommender.Models.ArtistSimilarToArtist() { Artist1 = artist, Artist2 = sa.Key, Ratio = sa.Value });
                }
                db.SaveChanges();
                ViewBag.Message = "success: maintained artist:" + artistName ;
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult ArtistInfo(string artistName)
        {
            using (RecommenderContext db = new RecommenderContext())
            {
                var artist = db.Artists.Where(a => a.Name == artistName).FirstOrDefault();
                var similarArtists = db.ArtistToArtist.Where(a => a.Artist1ID == artist.ID).Select(a => a.Artist2).ToList() ;

                return View("ArtistInfo", similarArtists);
            }
        }
    }
}