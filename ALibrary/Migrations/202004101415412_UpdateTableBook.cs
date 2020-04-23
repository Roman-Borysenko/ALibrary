namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookPath", c => c.String());
            DropColumn("dbo.Books", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Country", c => c.String());
            DropColumn("dbo.Books", "BookPath");
        }
    }
}
