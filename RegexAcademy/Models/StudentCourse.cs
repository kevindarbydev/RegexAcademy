using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegexAcademy.Models
{
    public class StudentCourse
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string CourseId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Index]
        public int Grade { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

    }
}
