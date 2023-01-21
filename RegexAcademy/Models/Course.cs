using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegexAcademy.Models
{
    public class Course
    {
        public Course()
        {

        }
        public Course(string courseName, DateTime startDate, DateTime endDate, string weekday, DateTime starttime, DateTime endtime)
        {
            CourseName = courseName;
            StartDate = startDate;
            EndDate = endDate;
            Weekday = weekday;
            StartTime = starttime;
            EndTime = endtime;
        }


        [Key]
        [StringLength(4)]
        public string CourseId { get; set; }

        public int? TeacherId { get; set; } // should be nullable as teachers can be assigned after course creation

        private string _courseName;
        [Required]
        [StringLength(30)]
        public string CourseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                // ensures first character is entered in db as capital letter, then trims leading and trailing whitespace
                string CapitalizeFirstCharCourseName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower());
                _courseName = CapitalizeFirstCharCourseName.Trim();
            }
        }

        private DateTime _startDate;
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
            }
        }

        public enum WeekdayEnum
        {
            Monday = 0,
            Tuesday = 1,
            Wednesday = 2,
            Thursday = 3,
            Friday = 4,
            Saturday = 5,
            Sunday = 6
        }

        [Required]
        public string Weekday { get; set; }

        private DateTime _startTime;
        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
            }
        }

        private DateTime _endTime;
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
            }
        }

        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }

        public virtual List<StudentCourse> StudentCourse { get; set; }

    }
}
