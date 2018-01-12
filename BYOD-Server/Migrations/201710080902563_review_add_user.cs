namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review_add_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OutletReviews",
                c => new
                    {
                        Oreview_id = c.Int(nullable: false, identity: true),
                        outlet_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Oreview_id)
                .ForeignKey("dbo.Outlets", t => t.outlet_id, cascadeDelete: true)
                .Index(t => t.outlet_id);
            
            AddColumn("dbo.Review_Ratings", "user_id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "date_joined", c => c.DateTime());
            AlterColumn("dbo.Orders", "order_time", c => c.DateTime());
            AlterColumn("dbo.Promocodes", "start_date", c => c.DateTime());
            AlterColumn("dbo.Promocodes", "expire_date", c => c.DateTime());
            AlterColumn("dbo.Outlets", "opening_time", c => c.DateTime());
            AlterColumn("dbo.Outlets", "closing_time", c => c.DateTime());
            AlterColumn("dbo.Outlets", "last_review_time", c => c.DateTime());
            AlterColumn("dbo.Review_Ratings", "review_time", c => c.DateTime());
            CreateIndex("dbo.Review_Ratings", "user_id");
            AddForeignKey("dbo.Review_Ratings", "user_id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutletReviews", "outlet_id", "dbo.Outlets");
            DropForeignKey("dbo.Review_Ratings", "user_id", "dbo.AspNetUsers");
            DropIndex("dbo.OutletReviews", new[] { "outlet_id" });
            DropIndex("dbo.Review_Ratings", new[] { "user_id" });
            AlterColumn("dbo.Review_Ratings", "review_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Outlets", "last_review_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Outlets", "closing_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Outlets", "opening_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Promocodes", "expire_date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Promocodes", "start_date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "order_time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "date_joined", c => c.DateTime(nullable: false));
            DropColumn("dbo.Review_Ratings", "user_id");
            DropTable("dbo.OutletReviews");
        }
    }
}
