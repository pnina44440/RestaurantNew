namespace RestaurantNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_2304 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        IdMenu = c.Int(nullable: false, identity: true),
                        Categorya = c.Int(nullable: false),
                        NameDose = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImageUri = c.String(),
                    })
                .PrimaryKey(t => t.IdMenu);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        IdOrder = c.Int(nullable: false, identity: true),
                        IdClub = c.Int(nullable: false),
                        IdMenu = c.Int(nullable: false),
                        DateForDay = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOrder)
                .ForeignKey("dbo.Menus", t => t.IdMenu, cascadeDelete: true)
                .Index(t => t.IdMenu);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "IdMenu", "dbo.Menus");
            DropIndex("dbo.Orders", new[] { "IdMenu" });
            DropTable("dbo.Orders");
            DropTable("dbo.Menus");
        }
    }
}
