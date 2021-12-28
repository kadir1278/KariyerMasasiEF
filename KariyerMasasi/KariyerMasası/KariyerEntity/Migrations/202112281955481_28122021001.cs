namespace KariyerEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28122021001 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Company", "BusinessAreaID", "dbo.BusinessArea");
            DropIndex("dbo.Company", new[] { "BusinessAreaID" });
            CreateTable(
                "dbo.CompanyBusinessArea",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        BusinessAreaID = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        DeletionStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BusinessArea", t => t.BusinessAreaID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID)
                .Index(t => t.BusinessAreaID);
            
            DropColumn("dbo.Company", "BusinessAreaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Company", "BusinessAreaID", c => c.Int(nullable: false));
            DropForeignKey("dbo.CompanyBusinessArea", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanyBusinessArea", "BusinessAreaID", "dbo.BusinessArea");
            DropIndex("dbo.CompanyBusinessArea", new[] { "BusinessAreaID" });
            DropIndex("dbo.CompanyBusinessArea", new[] { "CompanyID" });
            DropTable("dbo.CompanyBusinessArea");
            CreateIndex("dbo.Company", "BusinessAreaID");
            AddForeignKey("dbo.Company", "BusinessAreaID", "dbo.BusinessArea", "ID");
        }
    }
}
