namespace KariyerEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserLanguage", "LanguageID", "dbo.Language");
            DropIndex("dbo.UserLanguage", new[] { "LanguageID" });
            AddColumn("dbo.UserLanguage", "Language", c => c.String());
            AddColumn("dbo.UserReference", "CompanyName", c => c.String());
            AddColumn("dbo.UserReference", "Position", c => c.String());
            DropColumn("dbo.UserLanguage", "LanguageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserLanguage", "LanguageID", c => c.Int(nullable: false));
            DropColumn("dbo.UserReference", "Position");
            DropColumn("dbo.UserReference", "CompanyName");
            DropColumn("dbo.UserLanguage", "Language");
            CreateIndex("dbo.UserLanguage", "LanguageID");
            AddForeignKey("dbo.UserLanguage", "LanguageID", "dbo.Language", "ID");
        }
    }
}
