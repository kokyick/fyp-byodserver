namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StripeCards", new[] { "user_id" });
            RenameColumn(table: "dbo.StripeCards", name: "card_type", newName: "Card_type_id");
            RenameIndex(table: "dbo.StripeCards", name: "IX_card_type", newName: "IX_Card_type_id");
            CreateIndex("dbo.StripeCards", "User_id");
            DropColumn("dbo.StripeCards", "exp_month");
            DropColumn("dbo.StripeCards", "exp_year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StripeCards", "exp_year", c => c.String());
            AddColumn("dbo.StripeCards", "exp_month", c => c.String());
            DropIndex("dbo.StripeCards", new[] { "User_id" });
            RenameIndex(table: "dbo.StripeCards", name: "IX_Card_type_id", newName: "IX_card_type");
            RenameColumn(table: "dbo.StripeCards", name: "Card_type_id", newName: "card_type");
            CreateIndex("dbo.StripeCards", "user_id");
        }
    }
}
