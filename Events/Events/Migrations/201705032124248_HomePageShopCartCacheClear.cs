namespace Events.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HomePageShopCartCacheClear : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimePurchased = c.DateTime(nullable: false),
                        Fulfilled = c.Boolean(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.TicketModelEventModels",
                c => new
                    {
                        TicketModel_Id = c.Int(nullable: false),
                        EventModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TicketModel_Id, t.EventModel_Id })
                .ForeignKey("dbo.TicketModels", t => t.TicketModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.EventModels", t => t.EventModel_Id, cascadeDelete: true)
                .Index(t => t.TicketModel_Id)
                .Index(t => t.EventModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketModels", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketModelEventModels", "EventModel_Id", "dbo.EventModels");
            DropForeignKey("dbo.TicketModelEventModels", "TicketModel_Id", "dbo.TicketModels");
            DropIndex("dbo.TicketModelEventModels", new[] { "EventModel_Id" });
            DropIndex("dbo.TicketModelEventModels", new[] { "TicketModel_Id" });
            DropIndex("dbo.TicketModels", new[] { "CustomerId" });
            DropTable("dbo.TicketModelEventModels");
            DropTable("dbo.TicketModels");
        }
    }
}
