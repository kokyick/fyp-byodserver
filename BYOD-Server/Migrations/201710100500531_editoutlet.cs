namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editoutlet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Outlets", "streetname", c => c.String());
            AlterColumn("dbo.Outlets", "lat", c => c.Double(nullable: false));
            AlterColumn("dbo.Outlets", "lon", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Outlets", "lon", c => c.String());
            AlterColumn("dbo.Outlets", "lat", c => c.String());
            AlterColumn("dbo.Outlets", "streetname", c => c.Boolean(nullable: false));
        }
    }
}
