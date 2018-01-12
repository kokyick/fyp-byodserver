namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class featuredphoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Merchants", "merchant_photo", c => c.String());
            AddColumn("dbo.Outlets", "featured_photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outlets", "featured_photo");
            DropColumn("dbo.Merchants", "merchant_photo");
        }
    }
}
