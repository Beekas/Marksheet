namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marksheet1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marksheets", "ObtePM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "ObteTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "ComputerTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "ComputerPM", c => c.Int(nullable: false));
            DropColumn("dbo.Marksheets", "Optional2PM");
            DropColumn("dbo.Marksheets", "Optional2TM");
            DropColumn("dbo.Marksheets", "HasPractical2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marksheets", "HasPractical2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Marksheets", "Optional2TM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "Optional2PM", c => c.Int(nullable: false));
            DropColumn("dbo.Marksheets", "ComputerPM");
            DropColumn("dbo.Marksheets", "ComputerTM");
            DropColumn("dbo.Marksheets", "ObteTM");
            DropColumn("dbo.Marksheets", "ObtePM");
        }
    }
}
