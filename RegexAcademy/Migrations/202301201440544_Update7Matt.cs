namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update7Matt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "EndTime");
            DropColumn("dbo.Courses", "StartTime");
        }
    }
}
