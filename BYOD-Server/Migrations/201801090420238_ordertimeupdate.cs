namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordertimeupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "order_time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "order_time", c => c.DateTime());
        }
    }
}
