using System;
using System.Data.Entity;
using System.Linq;

namespace RegexAcademy
{
    public class RegexAcademyDbContext : DbContext
    {
        // Your context has been configured to use a 'RegexAcademyDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RegexAcademy.RegexAcademyDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RegexAcademyDbContext' 
        // connection string in the application configuration file.
        public RegexAcademyDbContext()
            : base("name=RegexAcademyDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}