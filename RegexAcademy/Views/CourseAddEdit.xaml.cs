using RegexAcademy.Models;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for CourseAdd.xaml
    /// </summary>
    public partial class CourseAddEdit : Window
    {
        private Course selectedCourse = null;


        public CourseAddEdit()
        {
            InitializeComponent();

            try
            {

                // sets the content of checkboxes
                Globals.dbContext = new RegexAcademyDbContext();
                CbxCoursesWeekdaysMonday.Content = Course.WeekdayEnum.Monday;
                CbxCoursesWeekdaysTuesday.Content = Course.WeekdayEnum.Tuesday;
                CbxCoursesWeekdaysWednesday.Content = Course.WeekdayEnum.Wednesday;
                CbxCoursesWeekdaysThursday.Content = Course.WeekdayEnum.Thursday;
                CbxCoursesWeekdaysFriday.Content = Course.WeekdayEnum.Friday;
                CbxCoursesWeekdaysSaturday.Content = Course.WeekdayEnum.Saturday;
                CbxCoursesWeekdaysSunday.Content = Course.WeekdayEnum.Sunday;

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database (1)\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }

        }
        // Constructor for EditCourse Window
        public CourseAddEdit(Course selectedCourse)
        {
            InitializeComponent();

            try
            {
                Globals.dbContext = new RegexAcademyDbContext();

                BtnUpdateCourse.Visibility = Visibility.Visible;
                BtnCourseDialogSave.Visibility = Visibility.Hidden;

                CbxCoursesWeekdaysMonday.Content = Course.WeekdayEnum.Monday;
                CbxCoursesWeekdaysTuesday.Content = Course.WeekdayEnum.Tuesday;
                CbxCoursesWeekdaysWednesday.Content = Course.WeekdayEnum.Wednesday;
                CbxCoursesWeekdaysThursday.Content = Course.WeekdayEnum.Thursday;
                CbxCoursesWeekdaysFriday.Content = Course.WeekdayEnum.Friday;
                CbxCoursesWeekdaysSaturday.Content = Course.WeekdayEnum.Saturday;
                CbxCoursesWeekdaysSunday.Content = Course.WeekdayEnum.Sunday;

            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database (2)\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
            if (selectedCourse != null)
            {

                this.selectedCourse = selectedCourse;
                TbxCourseCode.Text = selectedCourse.CourseId;
                TbxCourseName.Text = selectedCourse.CourseName;
                DpCoursesStartDate.SelectedDate = selectedCourse.StartDate;
                DpCoursesEndDate.SelectedDate = selectedCourse.EndDate;
                TpCoursesStartTime.SelectedTime = selectedCourse.StartTime;
                TpCoursesEndTime.SelectedTime = selectedCourse.EndTime;
                string weekday = selectedCourse.Weekday;

                if (weekday.Contains("Monday"))
                {
                    CbxCoursesWeekdaysMonday.IsChecked = true;
                }
                if (weekday.Contains("Tueday") || weekday.Contains(" Tuesday"))
                {
                    CbxCoursesWeekdaysTuesday.IsChecked = true;
                }
                if (weekday.Contains("Wednesay") || weekday.Contains(" Wednesday"))
                {
                    CbxCoursesWeekdaysWednesday.IsChecked = true;
                }
                if (weekday.Contains("Thursday") || weekday.Contains(" Thursday"))
                {
                    CbxCoursesWeekdaysThursday.IsChecked = true;
                }
                if (weekday.Contains("Friday") || weekday.Contains(" Friday"))
                {
                    CbxCoursesWeekdaysFriday.IsChecked = true;
                }
                if (weekday.Contains("Saturday") || weekday.Contains(" Saturday"))
                {
                    CbxCoursesWeekdaysSaturday.IsChecked = true;
                }
                if (weekday.Contains("Sunday") || weekday.Contains(" Sunday"))
                {
                    CbxCoursesWeekdaysSunday.IsChecked = true;
                }
                //if (CbxCoursesWeekdaysMonday.IsChecked == true)
                //{
                //    //selectedCourse.Weekday = selectedWeekday.ToString();
                //    CbxCoursesWeekdaysMonday.Content = selectedCourse.Weekday;
                //}
                //else if (CbxCoursesWeekdaysTuesday.IsChecked == true)
                //{
                //    selectedCourse.Weekday = selectedWeekday.ToString();

                //}
                //else if (CbxCoursesWeekdaysWednesday.IsChecked == true)
                //{
                //    selectedCourse.Weekday = selectedWeekday.ToString();

                //}
                //else if (CbxCoursesWeekdaysThursday.IsChecked == true)
                //{
                //    selectedCourse.Weekday = selectedWeekday.ToString();

                //}
                //else if (CbxCoursesWeekdaysFriday.IsChecked == true)
                //{
                //    selectedCourse.Weekday = selectedWeekday.ToString();

                //}
                //else if (CbxCoursesWeekdaysSaturday.IsChecked == true)
                //{
                //    selectedCourse.Weekday = selectedWeekday.ToString();

                //}
                //else if (CbxCoursesWeekdaysSunday.IsChecked == true)
                //{
                //    selectedCourse.Weekday = selectedWeekday.ToString();

                //}
                //else
                //{
                //    Console.WriteLine("wtf");
                //}




                //|| CbxCoursesWeekdaysTuesday != null || CbxCoursesWeekdaysWednesday != null || CbxCoursesWeekdaysThursday != null || CbxCoursesWeekdaysFriday != null || CbxCoursesWeekdaysSaturday != null || CbxCoursesWeekdaysSunday != null ||) { }
            }
        }

        private bool ValidateCourses()
        {
            try
            {
                // needed in date and time checks to ensure proper time length constraints
                DateTime courseExceedsTime = TpCoursesStartTime.SelectedTime.Value.AddHours(6.0);
                DateTime courseTimeTooShort = TpCoursesStartTime.SelectedTime.Value.AddHours(1.0);

                //!! just missing course code validation, confused about code vs id

                // course name validation:

                // checks that course name and course code begin with same character
                if (!TbxCourseName.Text.StartsWith(TbxCourseCode.Text.Substring(0, 1)))
                {
                    MessageBox.Show("Course name and course code must share the same starting character.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks course name length
                else if (TbxCourseName.Text.Length < 3 || TbxCourseName.Text.Length > 30)
                {
                    MessageBox.Show("Course name must be between 3 and 30 characters", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks for no special chars (whitespace ok)
                else if (!Regex.IsMatch(TbxCourseName.Text, @"^[a-zA-Z0-9 ]{3,30}$"))
                {
                    MessageBox.Show("Course name must not contain special characters", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }


                // datepickers validation:

                // checks if dates are the same
                else if (DpCoursesStartDate.SelectedDate.Equals(DpCoursesEndDate.SelectedDate))
                {
                    MessageBox.Show("Start and end dates cannot match", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks if end date was set before start date
                else if (DpCoursesEndDate.SelectedDate.Value.Date < DpCoursesStartDate.SelectedDate.Value.Date)
                {
                    MessageBox.Show("End date cannot precede the start date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks if user brute-forced dates beyond date ranges set in CourseAdd.xaml DatePicker properties
                else if (DpCoursesStartDate.SelectedDate.Value.Year < 2023 || DpCoursesStartDate.SelectedDate.Value.Year > 2025 || DpCoursesEndDate.SelectedDate.Value.Year < 2023 || DpCoursesEndDate.SelectedDate.Value.Year > 2025)
                {
                    MessageBox.Show("Dates must fall between 2023-01-01 and 2025-12-31.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }


                // time picker validation:

                // checks if times are the same
                else if (TpCoursesStartTime.SelectedTime.Equals(TpCoursesEndTime.SelectedTime))
                {
                    MessageBox.Show("Start and end times cannot match", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                //checks if end time was set before start time
                else if (TpCoursesEndTime.SelectedTime.Value.TimeOfDay < TpCoursesStartTime.SelectedTime.Value.TimeOfDay)
                {
                    MessageBox.Show("End time cannot precede the start time.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks if course time is longer than 6 hours
                else if (TpCoursesEndTime.SelectedTime.Value > courseExceedsTime)
                {
                    MessageBox.Show("Course duration cannot exceed 6 hours.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks if course time is shorter than 1 hour
                else if (TpCoursesEndTime.SelectedTime.Value < courseTimeTooShort)
                {
                    MessageBox.Show("Course duration cannot be under an hour", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks if time is between 9am-9pm, accounting for 1 hour course time minimum
                else if (TpCoursesStartTime.SelectedTime.Value.Hour < 9 || TpCoursesStartTime.SelectedTime.Value.Hour > 20 || TpCoursesEndTime.SelectedTime.Value.Hour > 21)
                {
                    MessageBox.Show("Course cannot run outside 9am or 9pm", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checks if any of the pickers are empty (date and time)
                else if (DpCoursesStartDate.SelectedDate == null || DpCoursesEndDate.SelectedDate == null || TpCoursesStartTime.SelectedTime == null || TpCoursesEndTime.SelectedTime == null)
                {
                    MessageBox.Show("Please ensure all dates and times are picked.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }


                //  checkbox validation:

                //  if all are empty
                else if (CbxCoursesWeekdaysMonday.IsChecked == false &&
                    CbxCoursesWeekdaysTuesday.IsChecked == false &&
                    CbxCoursesWeekdaysWednesday.IsChecked == false &&
                    CbxCoursesWeekdaysThursday.IsChecked == false &&
                    CbxCoursesWeekdaysFriday.IsChecked == false &&
                    CbxCoursesWeekdaysSaturday.IsChecked == false &&
                    CbxCoursesWeekdaysSunday.IsChecked == false)
                {
                    MessageBox.Show("Please pick at least one weekday.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // if all are checked
                else if (CbxCoursesWeekdaysMonday.IsChecked == true &&
                    CbxCoursesWeekdaysTuesday.IsChecked == true &&
                    CbxCoursesWeekdaysWednesday.IsChecked == true &&
                    CbxCoursesWeekdaysThursday.IsChecked == true &&
                    CbxCoursesWeekdaysFriday.IsChecked == true &&
                    CbxCoursesWeekdaysSaturday.IsChecked == true &&
                    CbxCoursesWeekdaysSunday.IsChecked == true)
                {
                    MessageBox.Show("Courses cannot take place on every single day of the week.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else
                {
                    // all validations passed
                    return true;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error validating inputs" + ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                return false;
            }
        }

        private void BtnCourseDialogSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = ValidateCourses();
                // if validation passes, entry to database commences 
                if (isValid)
                {
                    // takes content value of checkboxes and appends as strings
                    var cb = this.grid.Children.OfType<CheckBox>();
                    StringBuilder sb = new StringBuilder();

                    foreach (CheckBox chk in cb)
                    {
                        if (chk.IsChecked == true)
                        {
                            sb.Append(chk.Content.ToString() + " ");
                        }
                    }

                    // new updated entry 
                    Course newCourse = new Course { CourseId = TbxCourseCode.Text, CourseName = TbxCourseName.Text, StartDate = (DateTime)DpCoursesStartDate.SelectedDate, EndDate = (DateTime)DpCoursesEndDate.SelectedDate, Weekday = sb.ToString(), StartTime = (DateTime)TpCoursesStartTime.SelectedTime, EndTime = (DateTime)TpCoursesEndTime.SelectedTime };

                    Globals.dbContext.Courses.Add(newCourse);
                    int results = Globals.dbContext.SaveChanges();

                    if (results > 0)
                    {
                        MessageBox.Show(this, "Course added successfully!", "Regex Academy", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        this.DialogResult = true;
                    }

                    ResetFields();
                }

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database (3)\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //this.DialogResult = true;
        }

        private void BtnUpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = ValidateCourses();
                // if validation passes, entry to database commences 
                if (isValid)
                {
                    // takes content value of checkboxes and appends as strings
                    var cb = this.grid.Children.OfType<CheckBox>();
                    StringBuilder sb = new StringBuilder();

                    foreach (CheckBox chk in cb)
                    {
                        if (chk.IsChecked == true)
                        {
                            sb.Append(chk.Content.ToString() + " ");
                        }
                    }
                    selectedCourse.CourseId = TbxCourseCode.Text;
                    selectedCourse.CourseName = TbxCourseName.Text;
                    selectedCourse.StartDate = (DateTime)DpCoursesStartDate.SelectedDate;
                    selectedCourse.EndDate = (DateTime)DpCoursesEndDate.SelectedDate;
                    selectedCourse.Weekday = sb.ToString();
                    selectedCourse.StartTime = (DateTime)TpCoursesStartTime.SelectedTime;
                    selectedCourse.StartTime = (DateTime)TpCoursesEndTime.SelectedTime;

                    Globals.dbContext.SaveChanges();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database (4)\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // just in case, since successful Add or Edit should close the form
        public void ResetFields()
        {
            TbxCourseCode.Text = string.Empty;
            TbxCourseName.Text = string.Empty;
            DpCoursesStartDate.SelectedDate = null;
            DpCoursesEndDate.SelectedDate = null;
            TpCoursesStartTime.SelectedTime = null;
            TpCoursesEndTime.SelectedTime = null;
            CbxCoursesWeekdaysMonday.IsChecked = false;
            CbxCoursesWeekdaysTuesday.IsChecked = false;
            CbxCoursesWeekdaysWednesday.IsChecked = false;
            CbxCoursesWeekdaysThursday.IsChecked = false;
            CbxCoursesWeekdaysFriday.IsChecked = false;
            CbxCoursesWeekdaysSaturday.IsChecked = false;
            CbxCoursesWeekdaysSunday.IsChecked = false;
        }

    }
}
