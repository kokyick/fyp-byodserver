namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Promocodes", "discount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Promocodes", "discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
