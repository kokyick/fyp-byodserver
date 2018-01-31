namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foodadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodOrdereds", "newly_added", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "recently_changed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "recently_changed");
            DropColumn("dbo.FoodOrdereds", "newly_added");
        }
    }
}
