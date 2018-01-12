namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gstcharge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outlets", "gst", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Outlets", "servicecharge", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outlets", "servicecharge");
            DropColumn("dbo.Outlets", "gst");
        }
    }
}
