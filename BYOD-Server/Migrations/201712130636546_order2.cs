namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "promocode_id", "dbo.Promocodes");
            DropIndex("dbo.Orders", new[] { "promocode_id" });
            AddColumn("dbo.FoodOrdereds", "served", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "completed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "promocode_id", c => c.Int());
            CreateIndex("dbo.Orders", "promocode_id");
            AddForeignKey("dbo.Orders", "promocode_id", "dbo.Promocodes", "promocodes_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "promocode_id", "dbo.Promocodes");
            DropIndex("dbo.Orders", new[] { "promocode_id" });
            AlterColumn("dbo.Orders", "promocode_id", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "completed");
            DropColumn("dbo.FoodOrdereds", "served");
            CreateIndex("dbo.Orders", "promocode_id");
            AddForeignKey("dbo.Orders", "promocode_id", "dbo.Promocodes", "promocodes_id", cascadeDelete: true);
        }
    }
}
