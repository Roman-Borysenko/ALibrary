namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldNumberPagesForBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "NumberPages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "NumberPages");
        }
    }
}
