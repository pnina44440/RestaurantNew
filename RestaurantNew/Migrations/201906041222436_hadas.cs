namespace RestaurantNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hadas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        IdCity = c.Int(nullable: false, identity: true),
                        NameCity = c.String(),
                    })
                .PrimaryKey(t => t.IdCity);
            
            CreateTable(
                "dbo.CustUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateForDay = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        Menu_IdMenu = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.Menu_IdMenu)
                .ForeignKey("dbo.CustUsers", t => t.User_Id)
                .Index(t => t.Menu_IdMenu)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        IdMenu = c.Int(nullable: false, identity: true),
                        NameDose = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ImageUri = c.String(),
                        Categorya = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMenu);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Discount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuSales",
                c => new
                    {
                        MenuId = c.String(nullable: false, maxLength: 128),
                        SaleId = c.Int(nullable: false),
                        Menu_IdMenu = c.Int(),
                    })
                .PrimaryKey(t => new { t.MenuId, t.SaleId })
                .ForeignKey("dbo.Menus", t => t.Menu_IdMenu)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.Menu_IdMenu);
            
            CreateTable(
                "dbo.MenuSale",
                c => new
                    {
                        IdMenu = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdMenu, t.Id })
                .ForeignKey("dbo.Menus", t => t.IdMenu, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.Id, cascadeDelete: true)
                .Index(t => t.IdMenu)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuSales", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.MenuSales", "Menu_IdMenu", "dbo.Menus");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.CustUsers");
            DropForeignKey("dbo.MenuSale", "Id", "dbo.Sales");
            DropForeignKey("dbo.MenuSale", "IdMenu", "dbo.Menus");
            DropForeignKey("dbo.Orders", "Menu_IdMenu", "dbo.Menus");
            DropIndex("dbo.MenuSale", new[] { "Id" });
            DropIndex("dbo.MenuSale", new[] { "IdMenu" });
            DropIndex("dbo.MenuSales", new[] { "Menu_IdMenu" });
            DropIndex("dbo.MenuSales", new[] { "SaleId" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "Menu_IdMenu" });
            DropTable("dbo.MenuSale");
            DropTable("dbo.MenuSales");
            DropTable("dbo.Sales");
            DropTable("dbo.Menus");
            DropTable("dbo.Orders");
            DropTable("dbo.CustUsers");
            DropTable("dbo.Cities");
        }
    }
}
