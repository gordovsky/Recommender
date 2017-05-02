using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class Scrobble
    {
        [Key]
        public int ID { get; set; }
        public int Count { get; set; }
        public int TrackId { get; set; }
        public int UserId { get; set; }
        public Track Track { get; set; }
        public User User { get; set; }
    }
}
