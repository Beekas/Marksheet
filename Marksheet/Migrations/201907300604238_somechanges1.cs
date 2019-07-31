namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechanges1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marksheets", "HasPractical1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Marksheets", "HasPractical2", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Marksheets", "HasPractical2");
            DropColumn("dbo.Marksheets", "HasPractical1");
        }
    }
}
