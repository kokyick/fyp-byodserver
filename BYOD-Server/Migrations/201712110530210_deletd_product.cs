namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletd_product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MerchantProducts", "deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MerchantProducts", "deleted");
        }
    }
}
