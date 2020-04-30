namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelsArticleAndSimilarArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SimilarArticles", "Article_Id", c => c.Int());
            CreateIndex("dbo.SimilarArticles", "Article_Id");
            AddForeignKey("dbo.SimilarArticles", "Article_Id", "dbo.Articles", "Id");
            DropColumn("dbo.SimilarArticles", "ArticleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SimilarArticles", "ArticleId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SimilarArticles", "Article_Id", "dbo.Articles");
            DropIndex("dbo.SimilarArticles", new[] { "Article_Id" });
            DropColumn("dbo.SimilarArticles", "Article_Id");
        }
    }
}
