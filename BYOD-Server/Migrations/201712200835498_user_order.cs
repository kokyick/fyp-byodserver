namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "user_id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "user_id");
            AddForeignKey("dbo.Orders", "user_id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "user_id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "user_id" });
            DropColumn("dbo.Orders", "user_id");
        }
    }
}
