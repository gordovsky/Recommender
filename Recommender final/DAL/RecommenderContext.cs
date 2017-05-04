using Recommender.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Recommender_final.DAL
{
    public class RecommenderContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Artist_Tag> Artist_Tags { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Scrobble> Scrobbles { get; set; }
        public DbSet<ArtistSimilarToArtist> ArtistToArtist { get; set; }
        public DbSet<UserToUser> UserToUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ArtistSimilarToArtist>()
                .HasRequired(c => c.Artist1).WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<ArtistSimilarToArtist>()
                .HasRequired(c => c.Artist2).WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserToUser>()
                .HasRequired(c => c.User1).WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserToUser>()
                .HasRequired(c => c.User2).WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}