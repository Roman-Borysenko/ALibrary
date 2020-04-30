namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldViewForArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "View", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "View");
        }
    }
}
