namespace UWGBRestaurantAutomation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paidfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "Paid");
        }
    }
}
