namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewModelForAdvertising : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertising",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Image = c.String(),
                        Place = c.Boolean(nullable: false),
                        Create = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Advertising");
        }
    }
}
