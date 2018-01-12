namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.food_ordered",
                c => new
                    {
                        food_ordered_id = c.Int(nullable: false, identity: true),
                        outlet_product_id = c.Int(nullable: false),
                        order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.food_ordered_id)
                .ForeignKey("dbo.Orders", t => t.order_id, cascadeDelete: true)
                .ForeignKey("dbo.outlet_product", t => t.outlet_product_id, cascadeDelete: true)
                .Index(t => t.outlet_product_id)
                .Index(t => t.order_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        order_time = c.DateTime(nullable: false),
                        total_bill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        comments = c.String(),
                    })
                .PrimaryKey(t => t.order_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.food_ordered", "outlet_product_id", "dbo.outlet_product");
            DropForeignKey("dbo.food_ordered", "order_id", "dbo.Orders");
            DropIndex("dbo.food_ordered", new[] { "order_id" });
            DropIndex("dbo.food_ordered", new[] { "outlet_product_id" });
            DropTable("dbo.Orders");
            DropTable("dbo.food_ordered");
        }
    }
}
