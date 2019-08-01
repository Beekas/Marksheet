namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marksheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marksheets", "NepaliPM", c => c.Int(nullable: false));
            AddColumn("dbo.Marksheets", "SocialPM", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Marksheets", "SocialPM");
            DropColumn("dbo.Marksheets", "NepaliPM");
        }
    }
}
