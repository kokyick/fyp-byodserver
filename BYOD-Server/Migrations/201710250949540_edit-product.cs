namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MerchantProducts", "avg_ratings", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OutletProducts", "outlet_id", c => c.Int(nullable: false));
            CreateIndex("dbo.OutletProducts", "outlet_id");
            AddForeignKey("dbo.OutletProducts", "outlet_id", "dbo.Outlets", "outlet_id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutletProducts", "outlet_id", "dbo.Outlets");
            DropIndex("dbo.OutletProducts", new[] { "outlet_id" });
            DropColumn("dbo.OutletProducts", "outlet_id");
            DropColumn("dbo.MerchantProducts", "avg_ratings");
        }
    }
}
