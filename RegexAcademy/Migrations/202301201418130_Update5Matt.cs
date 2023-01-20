namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5Matt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Weekday", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Weekday");
        }
    }
}
