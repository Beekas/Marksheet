namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "SchoolId", "dbo.Schools");
            DropIndex("dbo.Students", new[] { "SchoolId" });
            RenameColumn(table: "dbo.Students", name: "SchoolId", newName: "School_Id");
            AlterColumn("dbo.Students", "School_Id", c => c.Int());
            CreateIndex("dbo.Students", "School_Id");
            AddForeignKey("dbo.Students", "School_Id", "dbo.Schools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "School_Id", "dbo.Schools");
            DropIndex("dbo.Students", new[] { "School_Id" });
            AlterColumn("dbo.Students", "School_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Students", name: "School_Id", newName: "SchoolId");
            CreateIndex("dbo.Students", "SchoolId");
            AddForeignKey("dbo.Students", "SchoolId", "dbo.Schools", "Id", cascadeDelete: true);
        }
    }
}
