namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Review_Ratings",
                c => new
                    {
                        Review_Ratings_id = c.Int(nullable: false, identity: true),
                        review_time = c.DateTime(nullable: false),
                        ratings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        comments = c.String(),
                        outlet_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Review_Ratings_id)
                .ForeignKey("dbo.Outlets", t => t.outlet_id, cascadeDelete: true)
                .Index(t => t.outlet_id);
            
            AddColumn("dbo.Outlets", "last_review_time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Outlets", "avg_ratings", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Outlets", "total_comments", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review_Ratings", "outlet_id", "dbo.Outlets");
            DropIndex("dbo.Review_Ratings", new[] { "outlet_id" });
            DropColumn("dbo.Outlets", "total_comments");
            DropColumn("dbo.Outlets", "avg_ratings");
            DropColumn("dbo.Outlets", "last_review_time");
            DropTable("dbo.Review_Ratings");
        }
    }
}
