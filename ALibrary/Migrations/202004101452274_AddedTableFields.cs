namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTableFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "View", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Create", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Create");
            DropColumn("dbo.Books", "Rating");
            DropColumn("dbo.Books", "View");
        }
    }
}
