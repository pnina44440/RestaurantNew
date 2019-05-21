namespace RestaurantNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New2304 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cities");
        }
    }
}
