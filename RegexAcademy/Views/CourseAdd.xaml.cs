using System;
using System.Windows;

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
                Globals.dbContext = new RegexAcademyDbContext(); // Exceptions
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
                if (DpCoursesStartDate.SelectedDate == null || DpCoursesEndDate.SelectedDate == null || TpCoursesStartTime == null || TpCoursesEndTime == null)
                {
                    MessageBox.Show("Please ensure all dates and times are picked.");
                    return;
                }
                Models.Course newCourse = new Models.Course { CourseName = TbxCourseName.Text, StartDate = (DateTime)DpCoursesStartDate.SelectedDate, EndDate = (DateTime)DpCoursesEndDate.SelectedDate, StartTime = (DateTime)TpCoursesStartTime.SelectedTime, EndTime = (DateTime)TpCoursesEndTime.SelectedTime };
                Globals.dbContext.Courses.Add(newCourse);
                Globals.dbContext.SaveChanges();
                MessageBox.Show(this, "Course saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //ResetFields();

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

            this.DialogResult = true;
        }


    }
}
