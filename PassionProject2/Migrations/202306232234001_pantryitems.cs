namespace PassionProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pantryitems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PantryItems",
                c => new
                    {
                        PantryItemID = c.Int(nullable: false, identity: true),
                        PantryItemName = c.String(),
                        PantryItemUnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PantryItemCurrentQty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PantryItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PantryItems");
        }
    }
}
