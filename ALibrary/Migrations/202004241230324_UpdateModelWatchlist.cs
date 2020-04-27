namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelWatchlist : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Watchlists", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Watchlists", "UserId", c => c.Int(nullable: false));
        }
    }
}
