namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chris : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Weekday", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "CourseName", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Courses", "DayOfWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "DayOfWeek", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "CourseName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Courses", "Weekday");
        }
    }
}
