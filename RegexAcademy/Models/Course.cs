using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegexAcademy.Models
{
    public class Course
    {


        [Key]
        [StringLength(4)]
        public string CourseId { get; set; }

        public int TeacherId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public enum Weekdays { M = 1, Tu = 2, W = 3, Th = 4, F = 5, Sa = 6, Su = 7 };

        [Required]
        [EnumDataType(typeof(Weekdays))]
        public Weekdays DayOfWeek { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }

        public virtual List<StudentCourse> StudentCourse { get; set; }

    }
}
