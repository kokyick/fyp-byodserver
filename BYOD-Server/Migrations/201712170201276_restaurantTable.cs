namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restaurantTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantTables",
                c => new
                    {
                        table_id = c.Int(nullable: false, identity: true),
                        capacity = c.Int(nullable: false),
                        outlet_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.table_id)
                .ForeignKey("dbo.Outlets", t => t.outlet_id, cascadeDelete: true)
                .Index(t => t.outlet_id);
            
            AddColumn("dbo.Orders", "table_id", c => c.Int());
            CreateIndex("dbo.Orders", "table_id");
            AddForeignKey("dbo.Orders", "table_id", "dbo.RestaurantTables", "table_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantTables", "outlet_id", "dbo.Outlets");
            DropForeignKey("dbo.Orders", "table_id", "dbo.RestaurantTables");
            DropIndex("dbo.RestaurantTables", new[] { "outlet_id" });
            DropIndex("dbo.Orders", new[] { "table_id" });
            DropColumn("dbo.Orders", "table_id");
            DropTable("dbo.RestaurantTables");
        }
    }
}
