namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dishcomments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodOrdereds", "comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodOrdereds", "comments");
        }
    }
}
