namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldForModelBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ForAuthorize", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ForAuthorize");
        }
    }
}
