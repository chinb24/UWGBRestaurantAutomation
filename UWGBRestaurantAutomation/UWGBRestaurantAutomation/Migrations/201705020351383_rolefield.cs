namespace UWGBRestaurantAutomation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "Role");
        }
    }
}
