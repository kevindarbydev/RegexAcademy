namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stev : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StudentCourses", new[] { "Grade" });
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.StudentCourses", "Grade", c => c.Int());
            AddPrimaryKey("dbo.Users", "Username");
            CreateIndex("dbo.StudentCourses", "Grade");
            DropColumn("dbo.Users", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.StudentCourses", new[] { "Grade" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.StudentCourses", "Grade", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Password");
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.StudentCourses", "Grade");
        }
    }
}
