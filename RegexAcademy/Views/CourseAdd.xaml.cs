using RegexAcademy.Models;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for CourseAdd.xaml
    /// </summary>
    public partial class CourseAdd : Window
    {

        public CourseAdd()
        {
            InitializeComponent();
            try
            {

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
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private bool ValidateCourses()
        {
            try
            {
                // needed for conditional preventing courses from exceeding 6 hours
                DateTime courseExceedsTime = TpCoursesStartTime.SelectedTime.Value.AddHours(6.0);
                // needed for conditional preventing courses from being shorter than 1 hour
                DateTime courseTimeTooShort = TpCoursesStartTime.SelectedTime.Value.AddHours(1.0);

                // datepickers & timepickers validation
                if (DpCoursesStartDate.SelectedDate == null || DpCoursesEndDate.SelectedDate == null || TpCoursesStartTime.SelectedTime == null || TpCoursesEndTime.SelectedTime == null)
                {
                    MessageBox.Show("Please ensure all dates and times are picked.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else if (DpCoursesStartDate.SelectedDate.Equals(DpCoursesEndDate.SelectedDate))
                {
                    MessageBox.Show("Start and end dates cannot match", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else if (TpCoursesStartTime.SelectedTime.Equals(TpCoursesEndTime.SelectedTime))
                {
                    MessageBox.Show("Start and end times cannot match", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else if (DpCoursesEndDate.SelectedDate.Value.Date < DpCoursesStartDate.SelectedDate.Value.Date)
                {
                    MessageBox.Show("End date cannot precede the start date.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                else if (TpCoursesEndTime.SelectedTime.Value.TimeOfDay < TpCoursesStartTime.SelectedTime.Value.TimeOfDay)
                {
                    MessageBox.Show("End time cannot precede the start time.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }
                else if (TpCoursesEndTime.SelectedTime.Value.TimeOfDay > courseExceedsTime.TimeOfDay)
                {
                    MessageBox.Show("Course duration cannot exceed 6 hours.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;

                }
                else if (TpCoursesEndTime.SelectedTime.Value.TimeOfDay < courseTimeTooShort.TimeOfDay)
                {
                    MessageBox.Show("Course duration cannot be under an hour", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return false;
                }

                // checkboxes: if all are empty, throw error 
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

                    // checkboxes: if all are checked, throw error
                }
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
                if (isValid)
                {

                    var cb = this.grid.Children.OfType<CheckBox>();
                    StringBuilder sb = new StringBuilder();

                    foreach (CheckBox chk in cb)
                    {
                        if (chk.IsChecked == true)
                        {
                            sb.Append(chk.Content.ToString() + " ");
                        }
                    }

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
                MessageBox.Show(this, "Error reading from database (2)\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //this.DialogResult = true;
        }

        public void ResetFields()
        {
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
// applicable for file export
//List<CheckBox> CheckBoxes = new List<CheckBox> { CbxCoursesWeekdaysMonday, CbxCoursesWeekdaysTuesday, CbxCoursesWeekdaysWednesday, CbxCoursesWeekdaysThursday, CbxCoursesWeekdaysFriday , CbxCoursesWeekdaysSaturday , CbxCoursesWeekdaysSunday };
//string weekdays = string.Join(",", CheckBoxes.Where(Checkbox => Checkbox.IsChecked == true).Select(cb => cb.Content));
