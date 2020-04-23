namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldSlug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Slug", c => c.String());
            AddColumn("dbo.Categories", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Slug");
            DropColumn("dbo.Books", "Slug");
        }
    }
}
