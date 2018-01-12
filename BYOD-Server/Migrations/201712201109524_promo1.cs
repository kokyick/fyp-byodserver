namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promo1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Promocodes", "promo_conditions", "dbo.PromoConditions");
            DropIndex("dbo.Promocodes", new[] { "promo_conditions" });
            AddColumn("dbo.Promocodes", "promocode_name", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
