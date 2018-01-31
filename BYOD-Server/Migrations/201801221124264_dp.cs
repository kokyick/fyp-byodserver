namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "displaypic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "displaypic");
        }
    }
}
