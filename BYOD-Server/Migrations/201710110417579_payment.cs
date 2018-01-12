namespace BYOD_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.food_ordered", newName: "FoodOrdereds");
            RenameTable(name: "dbo.Promo_condition", newName: "PromoConditions");
            RenameTable(name: "dbo.outlet_product", newName: "OutletProducts");
            RenameTable(name: "dbo.merchant_product", newName: "MerchantProducts");
            RenameTable(name: "dbo.food_type", newName: "FoodTypes");
            RenameTable(name: "dbo.merchant_category", newName: "MerchantCategories");
            RenameTable(name: "dbo.Review_Ratings", newName: "ReviewRatings");
            CreateTable(
                "dbo.PaymentHistories",
                c => new
                    {
                        paymentHistory_id = c.Int(nullable: false, identity: true),
                        payment_time = c.DateTime(),
                        amouunt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                        user_id = c.String(maxLength: 128),
                        card_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.paymentHistory_id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id)
                .ForeignKey("dbo.StripeCards", t => t.card_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.card_id);
            
            CreateTable(
                "dbo.StripeCards",
                c => new
                    {
                        card_id = c.Int(nullable: false, identity: true),
                        last_four_digit = c.Int(nullable: false),
                        exp_month = c.String(),
                        exp_year = c.String(),
                        payment_key = c.String(),
                        user_id = c.String(maxLength: 128),
                        card_type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.card_id)
                .ForeignKey("dbo.CardTypes", t => t.card_type, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id)
                .Index(t => t.user_id)
                .Index(t => t.card_type);
            
            CreateTable(
                "dbo.CardTypes",
                c => new
                    {
                        card_type_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.card_type_id);
            
            AddColumn("dbo.Orders", "payment_id", c => c.Int());
            CreateIndex("dbo.Orders", "payment_id");
            AddForeignKey("dbo.Orders", "payment_id", "dbo.PaymentHistories", "paymentHistory_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "payment_id", "dbo.PaymentHistories");
            DropForeignKey("dbo.PaymentHistories", "card_id", "dbo.StripeCards");
            DropForeignKey("dbo.StripeCards", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentHistories", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StripeCards", "card_type", "dbo.CardTypes");
            DropIndex("dbo.StripeCards", new[] { "card_type" });
            DropIndex("dbo.StripeCards", new[] { "user_id" });
            DropIndex("dbo.PaymentHistories", new[] { "card_id" });
            DropIndex("dbo.PaymentHistories", new[] { "user_id" });
            DropIndex("dbo.Orders", new[] { "payment_id" });
            DropColumn("dbo.Orders", "payment_id");
            DropTable("dbo.CardTypes");
            DropTable("dbo.StripeCards");
            DropTable("dbo.PaymentHistories");
            RenameTable(name: "dbo.ReviewRatings", newName: "Review_Ratings");
            RenameTable(name: "dbo.MerchantCategories", newName: "merchant_category");
            RenameTable(name: "dbo.FoodTypes", newName: "food_type");
            RenameTable(name: "dbo.MerchantProducts", newName: "merchant_product");
            RenameTable(name: "dbo.OutletProducts", newName: "outlet_product");
            RenameTable(name: "dbo.PromoConditions", newName: "Promo_condition");
            RenameTable(name: "dbo.FoodOrdereds", newName: "food_ordered");
        }
    }
}
