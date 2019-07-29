namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class academicYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AcademicYears", "ActiveYear", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AcademicYears", "ActiveYear");
        }
    }
}
