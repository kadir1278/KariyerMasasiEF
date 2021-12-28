namespace KariyerEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28122021004 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Company", new[] { "DepartmentID" });
            RenameColumn(table: "dbo.Company", name: "DepartmentID", newName: "Department_ID");
            AlterColumn("dbo.Company", "Department_ID", c => c.Int());
            CreateIndex("dbo.Company", "Department_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Company", new[] { "Department_ID" });
            AlterColumn("dbo.Company", "Department_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Company", name: "Department_ID", newName: "DepartmentID");
            CreateIndex("dbo.Company", "DepartmentID");
        }
    }
}
