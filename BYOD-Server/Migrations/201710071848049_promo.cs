namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promocodes",
                c => new
                    {
                        promocodes_id = c.Int(nullable: false, identity: true),
                        start_date = c.DateTime(nullable: false),
                        expire_date = c.DateTime(nullable: false),
                        discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        promo_conditions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.promocodes_id)
                .ForeignKey("dbo.Promo_condition", t => t.promo_conditions, cascadeDelete: true)
                .Index(t => t.promo_conditions);
            
            CreateTable(
                "dbo.Promo_condition",
                c => new
                    {
                        promocondi_id = c.Int(nullable: false, identity: true),
                        birthday_month = c.Boolean(nullable: false),
                        ratings = c.Boolean(nullable: false),
                        last_login = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.promocondi_id);
            
            AddColumn("dbo.Orders", "promocode_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "promocode_id");
            AddForeignKey("dbo.Orders", "promocode_id", "dbo.Promocodes", "promocodes_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promocodes", "promo_conditions", "dbo.Promo_condition");
            DropForeignKey("dbo.Orders", "promocode_id", "dbo.Promocodes");
            DropIndex("dbo.Promocodes", new[] { "promo_conditions" });
            DropIndex("dbo.Orders", new[] { "promocode_id" });
            DropColumn("dbo.Orders", "promocode_id");
            DropTable("dbo.Promo_condition");
            DropTable("dbo.Promocodes");
        }
    }
}
