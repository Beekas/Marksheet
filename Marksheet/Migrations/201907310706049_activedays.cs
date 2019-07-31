namespace Marksheet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activedays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiveDays", "Activeday", c => c.Int(nullable: false));
            DropColumn("dbo.ActiveDays", "Activedays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActiveDays", "Activedays", c => c.Int(nullable: false));
            DropColumn("dbo.ActiveDays", "Activeday");
        }
    }
}
