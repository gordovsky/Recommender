﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Scrobble> Scrobbles { get; set; }
        public virtual ICollection<UserToUser> SimilarUsers { get; set; }
    }
}
