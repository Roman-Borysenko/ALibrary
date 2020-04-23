namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldImageForBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Image");
        }
    }
}
