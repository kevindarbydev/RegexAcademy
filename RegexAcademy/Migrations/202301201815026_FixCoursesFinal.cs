namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCoursesFinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Weekday", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Weekday", c => c.Int(nullable: false));
        }
    }
}
