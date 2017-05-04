using Recommender.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recommender.Models
{
    public class ArtistSimilarToArtist
    {
        [Key]
        public int ID { get; set; }
        public int Artist1ID { get; set; }
        public int Artist2ID { get; set; }
        public double Ratio { get; set; }
        public Artist Artist1 { get; set; }
        public Artist Artist2 { get; set; }
    }
}