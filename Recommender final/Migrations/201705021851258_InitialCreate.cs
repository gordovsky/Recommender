namespace Recommender_final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artist_Tag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArtistID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ArtistID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Scrobbles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        TrackId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tracks", t => t.TrackId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TrackId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Artist_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.Artist_ID)
                .Index(t => t.Artist_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scrobbles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Scrobbles", "TrackId", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "Artist_ID", "dbo.Artists");
            DropForeignKey("dbo.Artist_Tag", "TagID", "dbo.Tags");
            DropForeignKey("dbo.Artist_Tag", "ArtistID", "dbo.Artists");
            DropIndex("dbo.Tracks", new[] { "Artist_ID" });
            DropIndex("dbo.Scrobbles", new[] { "UserId" });
            DropIndex("dbo.Scrobbles", new[] { "TrackId" });
            DropIndex("dbo.Artist_Tag", new[] { "TagID" });
            DropIndex("dbo.Artist_Tag", new[] { "ArtistID" });
            DropTable("dbo.Users");
            DropTable("dbo.Tracks");
            DropTable("dbo.Scrobbles");
            DropTable("dbo.Tags");
            DropTable("dbo.Artists");
            DropTable("dbo.Artist_Tag");
        }
    }
}
