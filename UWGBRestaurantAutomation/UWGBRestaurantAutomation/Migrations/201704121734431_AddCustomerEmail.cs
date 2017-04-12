namespace UWGBRestaurantAutomation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "CustomerEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "CustomerEmail");
        }
    }
}
