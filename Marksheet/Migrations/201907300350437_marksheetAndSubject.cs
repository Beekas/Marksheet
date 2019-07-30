namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marksheetAndSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marksheets", "EnglishPM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "EnglishTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "NepaliTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "MathTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "SocialTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "HealthPM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "HealthTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "SciencePM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "ScienceTM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "Optional1PM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "Optional1TM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "Optional2PM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "Optional2TM", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "HasPractical", c => c.Boolean(nullable: false));
            DropColumn("dbo.Marksheets", "PMarks");
            DropColumn("dbo.Marksheets", "TMarks");
            DropColumn("dbo.Marksheets", "PGrade");
            DropColumn("dbo.Marksheets", "TGrade");
            DropColumn("dbo.Marksheets", "AggMarks");
            DropColumn("dbo.Marksheets", "AggGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marksheets", "AggGrade", c => c.String());
            AddColumn("dbo.Marksheets", "AggMarks", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "TGrade", c => c.String());
            AddColumn("dbo.Marksheets", "PGrade", c => c.String());
            AddColumn("dbo.Marksheets", "TMarks", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "PMarks", c => c.Int());
            DropColumn("dbo.Subjects", "HasPractical");
            DropColumn("dbo.Marksheets", "Optional2TM");
            DropColumn("dbo.Marksheets", "Optional2PM");
            DropColumn("dbo.Marksheets", "Optional1TM");
            DropColumn("dbo.Marksheets", "Optional1PM");
            DropColumn("dbo.Marksheets", "ScienceTM");
            DropColumn("dbo.Marksheets", "SciencePM");
            DropColumn("dbo.Marksheets", "HealthTM");
            DropColumn("dbo.Marksheets", "HealthPM");
            DropColumn("dbo.Marksheets", "SocialTM");
            DropColumn("dbo.Marksheets", "MathTM");
            DropColumn("dbo.Marksheets", "NepaliTM");
            DropColumn("dbo.Marksheets", "EnglishTM");
            DropColumn("dbo.Marksheets", "EnglishPM");
        }
    }
}
