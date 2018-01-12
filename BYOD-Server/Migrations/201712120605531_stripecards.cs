namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stripecards : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StripeCards", "Last_four_digit", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StripeCards", "Last_four_digit", c => c.Int(nullable: false));
        }
    }
}
