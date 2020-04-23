namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldTodayBestChoiceInCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "TodayBestChoice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "TodayBestChoice");
        }
    }
}
