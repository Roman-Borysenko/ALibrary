namespace ALibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCountry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Create = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Authors", "Country_Id", c => c.Int());
            CreateIndex("dbo.Authors", "Country_Id");
            AddForeignKey("dbo.Authors", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Authors", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Authors", new[] { "Country_Id" });
            DropColumn("dbo.Authors", "Country_Id");
            DropTable("dbo.Countries");
        }
    }
}
