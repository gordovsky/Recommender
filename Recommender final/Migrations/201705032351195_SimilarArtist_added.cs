namespace Recommender_final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SimilarArtist_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistSimilarToArtists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Artist1ID = c.Int(nullable: false),
                        Artist2ID = c.Int(nullable: false),
                        Ratio = c.Double(nullable: false),
                        Artist_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.Artist1ID)
                .ForeignKey("dbo.Artists", t => t.Artist2ID)
                .ForeignKey("dbo.Artists", t => t.Artist_ID)
                .Index(t => t.Artist1ID)
                .Index(t => t.Artist2ID)
                .Index(t => t.Artist_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistSimilarToArtists", "Artist_ID", "dbo.Artists");
            DropForeignKey("dbo.ArtistSimilarToArtists", "Artist2ID", "dbo.Artists");
            DropForeignKey("dbo.ArtistSimilarToArtists", "Artist1ID", "dbo.Artists");
            DropIndex("dbo.ArtistSimilarToArtists", new[] { "Artist_ID" });
            DropIndex("dbo.ArtistSimilarToArtists", new[] { "Artist2ID" });
            DropIndex("dbo.ArtistSimilarToArtists", new[] { "Artist1ID" });
            DropTable("dbo.ArtistSimilarToArtists");
        }
    }
}
