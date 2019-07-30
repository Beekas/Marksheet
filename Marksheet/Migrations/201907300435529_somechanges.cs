namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "School_Id", "dbo.Schools");
            DropIndex("dbo.Students", new[] { "School_Id" });
            RenameColumn(table: "dbo.Students", name: "School_Id", newName: "SchoolId");
            AlterColumn("dbo.Students", "SchoolId", c => c.Int(nullable: false));
            AlterColumn("dbo.Marksheets", "AcedamicYearId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "SchoolId");
            AddForeignKey("dbo.Students", "SchoolId", "dbo.Schools", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "SchooId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "SchooId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Students", "SchoolId", "dbo.Schools");
            DropIndex("dbo.Students", new[] { "SchoolId" });
            AlterColumn("dbo.Marksheets", "AcedamicYearId", c => c.String());
            AlterColumn("dbo.Students", "SchoolId", c => c.Int());
            RenameColumn(table: "dbo.Students", name: "SchoolId", newName: "School_Id");
            CreateIndex("dbo.Students", "School_Id");
            AddForeignKey("dbo.Students", "School_Id", "dbo.Schools", "Id");
        }
    }
}
