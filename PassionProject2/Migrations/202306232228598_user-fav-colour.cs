namespace PassionProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userfavcolour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FavColour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FavColour");
        }
    }
}
