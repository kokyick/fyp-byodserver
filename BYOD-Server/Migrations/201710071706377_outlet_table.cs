namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class outlet_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Outlets",
                c => new
                    {
                        outlet_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        streetname = c.Boolean(nullable: false),
                        unit_no = c.String(),
                        postal_code = c.String(),
                        contact_no = c.String(),
                        lat = c.String(),
                        lon = c.String(),
                        opening_time = c.DateTime(nullable: false),
                        closing_time = c.DateTime(nullable: false),
                        opening_status = c.Boolean(nullable: false),
                        merchant_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.outlet_id)
                .ForeignKey("dbo.Merchants", t => t.merchant_id, cascadeDelete: true)
                .Index(t => t.merchant_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Outlets", "merchant_id", "dbo.Merchants");
            DropIndex("dbo.Outlets", new[] { "merchant_id" });
            DropTable("dbo.Outlets");
        }
    }
}
