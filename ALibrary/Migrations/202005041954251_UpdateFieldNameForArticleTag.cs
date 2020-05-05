namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFieldNameForArticleTag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleTags", "Name", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleTags", "Name", c => c.String());
        }
    }
}
