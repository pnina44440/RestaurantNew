namespace RestaurantNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hemi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "IdMenu", "dbo.Menus");
            DropIndex("dbo.Orders", new[] { "IdMenu" });
            RenameColumn(table: "dbo.Orders", name: "IdMenu", newName: "Menu_IdMenu");
            DropPrimaryKey("dbo.Orders");
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "User_Id", c => c.Int());
            AlterColumn("dbo.Orders", "Menu_IdMenu", c => c.Int());
            AddPrimaryKey("dbo.Orders", "Id");
            CreateIndex("dbo.Orders", "Menu_IdMenu");
            CreateIndex("dbo.Orders", "User_Id");
            AddForeignKey("dbo.Orders", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Orders", "Menu_IdMenu", "dbo.Menus", "IdMenu");
            DropColumn("dbo.Orders", "IdOrder");
            DropColumn("dbo.Orders", "IdClub");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IdClub", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "IdOrder", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Orders", "Menu_IdMenu", "dbo.Menus");
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "Menu_IdMenu" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "Menu_IdMenu", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "User_Id");
            DropColumn("dbo.Orders", "Id");
            DropTable("dbo.Users");
            AddPrimaryKey("dbo.Orders", "IdOrder");
            RenameColumn(table: "dbo.Orders", name: "Menu_IdMenu", newName: "IdMenu");
            CreateIndex("dbo.Orders", "IdMenu");
            AddForeignKey("dbo.Orders", "IdMenu", "dbo.Menus", "IdMenu", cascadeDelete: true);
        }
    }
}
