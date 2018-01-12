namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.merchant_category",
                c => new
                    {
                        merchantCat_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.merchantCat_id);
            
            CreateTable(
                "dbo.merchant_product",
                c => new
                    {
                        merchant_product_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        product_image = c.String(),
                        food_type = c.Int(nullable: false),
                        merchant_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.merchant_product_id)
                .ForeignKey("dbo.food_type", t => t.food_type, cascadeDelete: true)
                .ForeignKey("dbo.Merchants", t => t.merchant_id, cascadeDelete: true)
                .Index(t => t.food_type)
                .Index(t => t.merchant_id);
            
            CreateTable(
                "dbo.food_type",
                c => new
                    {
                        type_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.type_id);
            
            CreateTable(
                "dbo.outlet_product",
                c => new
                    {
                        outlet_product_id = c.Int(nullable: false, identity: true),
                        merchant_product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.outlet_product_id)
                .ForeignKey("dbo.merchant_product", t => t.merchant_product_id, cascadeDelete: true)
                .Index(t => t.merchant_product_id);
            
            AddColumn("dbo.Merchants", "merchant_cat", c => c.Int(nullable: false));
            CreateIndex("dbo.Merchants", "merchant_cat");
            AddForeignKey("dbo.Merchants", "merchant_cat", "dbo.merchant_category", "merchantCat_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.outlet_product", "merchant_product_id", "dbo.merchant_product");
            DropForeignKey("dbo.merchant_product", "merchant_id", "dbo.Merchants");
            DropForeignKey("dbo.merchant_product", "food_type", "dbo.food_type");
            DropForeignKey("dbo.Merchants", "merchant_cat", "dbo.merchant_category");
            DropIndex("dbo.outlet_product", new[] { "merchant_product_id" });
            DropIndex("dbo.merchant_product", new[] { "merchant_id" });
            DropIndex("dbo.merchant_product", new[] { "food_type" });
            DropIndex("dbo.Merchants", new[] { "merchant_cat" });
            DropColumn("dbo.Merchants", "merchant_cat");
            DropTable("dbo.outlet_product");
            DropTable("dbo.food_type");
            DropTable("dbo.merchant_product");
            DropTable("dbo.merchant_category");
        }
    }
}
