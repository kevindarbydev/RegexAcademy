namespace RegexAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6Matt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            AlterColumn("dbo.Courses", "TeacherId", c => c.Int());
            CreateIndex("dbo.Courses", "TeacherId");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            AlterColumn("dbo.Courses", "TeacherId", c => c.Int(nullable: true));
            CreateIndex("dbo.Courses", "TeacherId");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
    }
}
