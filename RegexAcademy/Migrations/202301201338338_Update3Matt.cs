namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3Matt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "Weekday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Weekday", c => c.Int(nullable: false));
        }
    }
}
