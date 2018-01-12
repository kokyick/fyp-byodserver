namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolpaied : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "paid");
        }
    }
}
