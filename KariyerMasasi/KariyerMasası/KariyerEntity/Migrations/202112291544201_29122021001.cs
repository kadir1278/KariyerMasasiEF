namespace KariyerEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29122021001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanySpecialType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        UserSpecialTypeID = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        DeletionStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .ForeignKey("dbo.UserSpecialType", t => t.UserSpecialTypeID)
                .Index(t => t.CompanyID)
                .Index(t => t.UserSpecialTypeID);
            
            CreateIndex("dbo.CompanyBusinessArea", "BusinessAreaID");
            CreateIndex("dbo.CompanyDepartment", "DepartmentID");
            AddForeignKey("dbo.CompanyBusinessArea", "BusinessAreaID", "dbo.BusinessArea", "ID");
            AddForeignKey("dbo.CompanyDepartment", "DepartmentID", "dbo.Department", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanySpecialType", "UserSpecialTypeID", "dbo.UserSpecialType");
            DropForeignKey("dbo.CompanySpecialType", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanyDepartment", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.CompanyBusinessArea", "BusinessAreaID", "dbo.BusinessArea");
            DropIndex("dbo.CompanySpecialType", new[] { "UserSpecialTypeID" });
            DropIndex("dbo.CompanySpecialType", new[] { "CompanyID" });
            DropIndex("dbo.CompanyDepartment", new[] { "DepartmentID" });
            DropIndex("dbo.CompanyBusinessArea", new[] { "BusinessAreaID" });
            DropTable("dbo.CompanySpecialType");
        }
    }
}
