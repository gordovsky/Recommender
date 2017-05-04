using Recommender.Models;
using Recommender_final.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recommender_final.Lib
{
    public class NBC
    {
        private int[] _tags;
        private int[] _artistTags;
        private Artist[] _artists;
        //private bool[] _matrix;
        private Artist[] _mostSimilarArtist;
        public NBC()
        {
            using (RecommenderContext db = new RecommenderContext())
            {
                _tags = db.Tags.Select(t => t.ID).ToArray();
                _artists = db.Artists.ToArray();
            }
        }
        public Dictionary<Artist, double> GetSimilarArtists(Artist artist)
        {
            //double result = 1;
            using (RecommenderContext db = new RecommenderContext())
            {
                _artistTags = db.Artists.Find(artist.ID).Artist_tags.Select(a => a.TagID).ToArray();
            

                Dictionary<Artist, double > topSimilar = new Dictionary<Artist, double>();
                for(int j = 0; j < _artists.Length - 1; j++)
                {
                    int[] otherArtistTags;
                    //using (RecommenderContext db = new RecommenderContext())
                    //{
                        otherArtistTags = db.Artists.Find(_artists[j].ID).Artist_tags.Select(x => x.TagID).ToArray(); //.Artist_tags.Select(x => x.TagID);
                    //}
                
                    var goodTagsCount = otherArtistTags.Intersect(_artistTags).Count();
                    var artistP = ((double)goodTagsCount) / _tags.Count();
                    topSimilar.Add(_artists[j], artistP);
                    topSimilar =  topSimilar.OrderByDescending(t => t.Value).Take(10).Select(t => new { t.Key, t.Value }).ToDictionary(t => t.Key, t=> t.Value);
                    //for (int i = 0; i < _tags.Length; i++)
                    //{
                    //    double tagP = 1;
                    //    double goodTagCount = 0;
                    //    if (_tags.Contains(_artists[j].))
                    //        goodTagCount++;
                    //    if (goodTagCount != 0)
                    //        tagP = goodTagCount / _artistTags.Length;
                    //}


                    //result = result * tagP;
                }

                return topSimilar;
            }
            //return result;
        }
    }
}