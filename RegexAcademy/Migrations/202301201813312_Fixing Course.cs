namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingCourse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Weekday", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Weekday", c => c.String(nullable: false));
        }
    }
}
