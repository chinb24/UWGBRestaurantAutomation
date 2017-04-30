namespace UWGBRestaurantAutomation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CCName = c.String(),
                        CCNumber = c.String(),
                        CCMonth = c.Int(nullable: false),
                        CCYear = c.Int(nullable: false),
                        CCSecurity = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payment");
        }
    }
}
