namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class outofstock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OutletProducts", "out_of_stock", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OutletProducts", "out_of_stock");
        }
    }
}
