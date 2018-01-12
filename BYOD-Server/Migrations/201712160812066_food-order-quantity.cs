namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foodorderquantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodOrdereds", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodOrdereds", "quantity");
        }
    }
}
