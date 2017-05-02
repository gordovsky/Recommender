using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class Artist_Tag
    {
        [Key]
        public int ID { get; set; }
        public int ArtistID { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }
        public Artist Artist { get; set; }
    }
}
