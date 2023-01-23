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
        public Course(string courseId, string courseName, DateTime startDate, DateTime endDate, string weekday, DateTime starttime, DateTime endtime)
        {
            CourseId = courseId;
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
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new ArgumentException("Course name must be between 3 and 30 characters");
                }
                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9 ]{3,30}$"))
                {
                    throw new ArgumentException("Course name cannot contain special characters.");
                }

                // ensures first character is entered in db as capital letter, then trims leading and trailing whitespace
                string CapitalizeFirstCharCourseName = string.Concat(value[0].ToString().ToUpper(), value.Substring(1).ToLower());
                _courseName = CapitalizeFirstCharCourseName;
            }
        }
        // checks if user brute-forced dates beyond date ranges set in CourseAdd.xaml DatePicker properties
        //else if (DpCoursesStartDate.SelectedDate.Value.Year< 2023 || DpCoursesStartDate.SelectedDate.Value.Year> 2025 || DpCoursesEndDate.SelectedDate.Value.Year< 2023 || DpCoursesEndDate.SelectedDate.Value.Year> 2025)
        //{
        //    MessageBox.Show("Dates must fall between 2023-01-01 and 2025-12-31.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
        //    return false;
        //}
        private DateTime _startDate;
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                //checks if user brute - forced dates beyond date ranges set in CourseAdd.xaml DatePicker properties
                if (value.Year < 2023 || value.Year > 2025)
                {
                    throw new ArgumentException("Dates must fall between 2023-01-01 and 2025-12-31.");
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
            {   //checks if user brute - forced dates beyond date ranges set in CourseAdd.xaml DatePicker properties
                if (value.Year < 2023 || value.Year > 2025)
                {
                    throw new ArgumentException("Dates must fall between 2023-01-01 and 2025-12-31.");
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

        public override string ToString()
        {
            return $"Course ID: {CourseId};Course Name: {CourseName};Day(s) of Week: {Weekday};Start Date: {StartDate};End Date: {EndDate};Start Time: {StartTime};End Time: {EndTime}; Taught by: {TeacherId}";
        }
    }
}
