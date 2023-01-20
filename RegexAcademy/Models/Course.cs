using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace RegexAcademy.Models
{
    public class Course
    {
        public Course()
        {

        }
        public Course(string courseName, DateTime startDate, DateTime endDate, string weekday, datetime starttime, datetime endtime)
        {
            CourseName = courseName;
            StartDate = startDate;
            EndDate = endDate;
            Weekday = weekday;
            //Weekday = (WeekdayEnum)Enum.Parse(typeof(WeekdayEnum), weekday);
            StartTime= starttime;
            EndTime= endtime;
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
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new ArgumentException("Course name length must be 3-30 characters long");
                }

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9 ]{3,30}$")) // this regex should only allow letters and numbers, and spaces
                {
                    throw new ArgumentException("Course name cannot contain special characters. ");
                }
                string CapitalizeFirstCharCourseName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower());
                _courseName = CapitalizeFirstCharCourseName;
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
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select a date.");
                }
                //!!!has to be within 2 weeks of current actual date
                if (value.Year < 1900 || value.Year > 2024)
                {
                    throw new ArgumentException("Please enter a valid start date.");
                }
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
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select a date.");
                }
                //!!!has to be at least one week after start date and cannot be before it
                if (value.Year < 1900 || value.Year > 2024)
                {
                    throw new ArgumentException("Please enter a valid end date.");
                }
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
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select a start time.");
                }
                //!!!has to be from 7am to 7pm
                //if ()
                //{
                //    throw new ArgumentException("Please enter a valid start time.");
                //}
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
                if (value == null)
                {
                    throw new ArgumentException("Please make sure to select an end time.");
                }
                //!!!has to be from 8am to 11pm
                //if ()
                //{
                //    throw new ArgumentException("Please enter a valid end time.");
                //}
                _endTime = value;
            }
        }

        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }

        public virtual List<StudentCourse> StudentCourse { get; set; }

    }
}
// missing tostring
