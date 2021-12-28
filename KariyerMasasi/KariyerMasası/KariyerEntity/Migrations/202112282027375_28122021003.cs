namespace KariyerEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28122021003 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CompanyBusinessArea", "BusinessAreaID", "dbo.BusinessArea");
            DropForeignKey("dbo.CompanyDepartment", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.CompanySpecialType", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.CompanySpecialType", "UserSpecialTypeID", "dbo.UserSpecialType");
            DropIndex("dbo.CompanyBusinessArea", new[] { "BusinessAreaID" });
            DropIndex("dbo.CompanyDepartment", new[] { "DepartmentID" });
            DropIndex("dbo.CompanySpecialType", new[] { "CompanyID" });
            DropIndex("dbo.CompanySpecialType", new[] { "UserSpecialTypeID" });
            DropTable("dbo.CompanySpecialType");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.CompanySpecialType", "UserSpecialTypeID");
            CreateIndex("dbo.CompanySpecialType", "CompanyID");
            CreateIndex("dbo.CompanyDepartment", "DepartmentID");
            CreateIndex("dbo.CompanyBusinessArea", "BusinessAreaID");
            AddForeignKey("dbo.CompanySpecialType", "UserSpecialTypeID", "dbo.UserSpecialType", "ID");
            AddForeignKey("dbo.CompanySpecialType", "CompanyID", "dbo.Company", "ID");
            AddForeignKey("dbo.CompanyDepartment", "DepartmentID", "dbo.Department", "ID");
            AddForeignKey("dbo.CompanyBusinessArea", "BusinessAreaID", "dbo.BusinessArea", "ID");
        }
    }
}
