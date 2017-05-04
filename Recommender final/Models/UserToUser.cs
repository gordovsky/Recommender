using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recommender.Models
{
    public class UserToUser
    {
        [Key]
        public int ID { get; set; }
        public int User1ID { get; set; }
        public int User2ID { get; set; }
        public double Ratio { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}