namespace UWGBRestaurantAutomation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productimagefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductImage");
        }
    }
}
