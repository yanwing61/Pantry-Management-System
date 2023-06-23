namespace PassionProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryID = c.Int(nullable: false, identity: true),
                        InventoryQty = c.Int(nullable: false),
                        InventoryLogDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inventories");
        }
    }
}
