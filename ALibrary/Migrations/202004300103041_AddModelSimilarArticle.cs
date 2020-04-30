namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelSimilarArticle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SimilarArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        SimilarArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SimilarArticles");
        }
    }
}
