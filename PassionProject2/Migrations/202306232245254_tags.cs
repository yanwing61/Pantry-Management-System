namespace PassionProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.TagPantryItems",
                c => new
                    {
                        Tag_TagID = c.Int(nullable: false),
                        PantryItem_PantryItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagID, t.PantryItem_PantryItemID })
                .ForeignKey("dbo.Tags", t => t.Tag_TagID, cascadeDelete: true)
                .ForeignKey("dbo.PantryItems", t => t.PantryItem_PantryItemID, cascadeDelete: true)
                .Index(t => t.Tag_TagID)
                .Index(t => t.PantryItem_PantryItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPantryItems", "PantryItem_PantryItemID", "dbo.PantryItems");
            DropForeignKey("dbo.TagPantryItems", "Tag_TagID", "dbo.Tags");
            DropIndex("dbo.TagPantryItems", new[] { "PantryItem_PantryItemID" });
            DropIndex("dbo.TagPantryItems", new[] { "Tag_TagID" });
            DropTable("dbo.TagPantryItems");
            DropTable("dbo.Tags");
        }
    }
}
