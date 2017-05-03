namespace Events.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataEntry2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventModels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventModels", "Description");
        }
    }
}
