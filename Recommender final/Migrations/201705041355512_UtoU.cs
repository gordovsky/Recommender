namespace Recommender_final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UtoU : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserToUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User1ID = c.Int(nullable: false),
                        User2ID = c.Int(nullable: false),
                        Ratio = c.Double(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User1ID)
                .ForeignKey("dbo.Users", t => t.User2ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User1ID)
                .Index(t => t.User2ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserToUsers", "User2ID", "dbo.Users");
            DropForeignKey("dbo.UserToUsers", "User1ID", "dbo.Users");
            DropIndex("dbo.UserToUsers", new[] { "User_ID" });
            DropIndex("dbo.UserToUsers", new[] { "User2ID" });
            DropIndex("dbo.UserToUsers", new[] { "User1ID" });
            DropTable("dbo.UserToUsers");
        }
    }
}
