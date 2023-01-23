using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for DataAnalyticsWindowStatistics.xaml
    /// </summary>
    public partial class DataAnalyticsWindowStatistics : Page
    {
        public DataAnalyticsWindowStatistics()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var countStudents = Globals.dbContext.Students.Count();
                var countTeachers = Globals.dbContext.Teachers.Where(t => t.Availability == true).Count();
                //checking for courses that are ongoing
                var countCourses = Globals.dbContext.Courses.Where(c => (c.StartDate <= DateTime.Today && c.EndDate >= DateTime.Today)).Count();


                LblStudentsEnrolled.Content = countStudents;
                LblTeachersAvailable.Content = countTeachers;
                LblCoursesAvailable.Content = countCourses;
            }
            catch (SystemException ex)
            {
                MessageBox.Show($"This should never occur : {ex.Message}, type: {ex.GetType().Name}");
            }
        }
    }
}
