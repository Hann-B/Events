namespace Events.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventModels", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventModels", "Price");
        }
    }
}
