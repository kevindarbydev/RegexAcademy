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
        private void BtnCourseDialogSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DpCoursesStartDate.SelectedDate == null || DpCoursesEndDate.SelectedDate == null || TpCoursesStartTime.SelectedTime == null || TpCoursesEndTime.SelectedTime == null)
                {
                    MessageBox.Show("Please ensure all dates and times are picked.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else if (CbxCoursesWeekdaysMonday.IsChecked == false &&
                    CbxCoursesWeekdaysTuesday.IsChecked == false &&
                    CbxCoursesWeekdaysWednesday.IsChecked == false &&
                    CbxCoursesWeekdaysThursday.IsChecked == false &&
                    CbxCoursesWeekdaysFriday.IsChecked == false &&
                    CbxCoursesWeekdaysSaturday.IsChecked == false &&
                    CbxCoursesWeekdaysSunday.IsChecked == false)
                {
                    MessageBox.Show("Please pick at least one weekday", "Input Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                //else
                //{
                //    MessageBox.Show("Error reading inputs", "Internal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    Console.WriteLine("Error reading course inputs - Internal Error");
                //}


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
                    MessageBox.Show(this, "Course added successfully!", "Update success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.DialogResult = true;
                }


                MessageBox.Show(this, "Course saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                ResetFields();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
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
