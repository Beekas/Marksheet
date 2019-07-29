namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FiscalYear = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActiveDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolId = c.Int(nullable: false),
                        TerminalName = c.String(),
                        Activedays = c.Int(nullable: false),
                        AcademicYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearId, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .Index(t => t.SchoolId)
                .Index(t => t.AcademicYearId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        Slogan = c.String(),
                        URL = c.String(),
                        State = c.String(),
                        Municipality = c.String(),
                        WardNo = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        Address = c.String(),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        Phone = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Mobile = c.String(),
                        SchooId = c.Int(nullable: false),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .Index(t => t.School_Id);
            
            CreateTable(
                "dbo.Marksheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        SchoolId = c.Int(),
                        PMarks = c.Int(),
                        TMarks = c.Int(nullable: false),
                        PGrade = c.String(),
                        TGrade = c.String(),
                        AggMarks = c.Int(nullable: false),
                        AggGrade = c.String(),
                        ResultStatus = c.Boolean(nullable: false),
                        AcedamicYearId = c.String(),
                        Term = c.String(),
                        Attendance = c.Int(nullable: false),
                        AcademicYear_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYear_Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.AcademicYear_Id);
            
            CreateTable(
                "dbo.OptionalSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SchoolId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubName = c.String(),
                        TFullMarks = c.Int(nullable: false),
                        PFullMarks = c.Int(),
                        PPassMarks = c.Int(nullable: false),
                        TPassMarks = c.Int(nullable: false),
                        Optional = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OptionalSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.OptionalSubjects", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Marksheets", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Marksheets", "AcademicYear_Id", "dbo.AcademicYears");
            DropForeignKey("dbo.ActiveDays", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Students", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.ActiveDays", "AcademicYearId", "dbo.AcademicYears");
            DropIndex("dbo.OptionalSubjects", new[] { "SubjectId" });
            DropIndex("dbo.OptionalSubjects", new[] { "SchoolId" });
            DropIndex("dbo.Marksheets", new[] { "AcademicYear_Id" });
            DropIndex("dbo.Marksheets", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "School_Id" });
            DropIndex("dbo.ActiveDays", new[] { "AcademicYearId" });
            DropIndex("dbo.ActiveDays", new[] { "SchoolId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.OptionalSubjects");
            DropTable("dbo.Marksheets");
            DropTable("dbo.Students");
            DropTable("dbo.Schools");
            DropTable("dbo.ActiveDays");
            DropTable("dbo.AcademicYears");
        }
    }
}
