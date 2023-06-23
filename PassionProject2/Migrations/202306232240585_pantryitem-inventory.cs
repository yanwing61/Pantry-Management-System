namespace PassionProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pantryiteminventory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "PantryItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.Inventories", "PantryItemID");
            AddForeignKey("dbo.Inventories", "PantryItemID", "dbo.PantryItems", "PantryItemID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "PantryItemID", "dbo.PantryItems");
            DropIndex("dbo.Inventories", new[] { "PantryItemID" });
            DropColumn("dbo.Inventories", "PantryItemID");
        }
    }
}
