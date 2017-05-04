﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class Artist
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Artist_Tag> Artist_tags { get; set; }
        public virtual ICollection<ArtistSimilarToArtist> SimilarArtists { get; set; }
    }
}
