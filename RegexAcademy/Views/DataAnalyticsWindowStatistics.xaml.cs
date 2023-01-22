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

            var countStudents = Globals.dbContext.Students.Count();
            var countTeachers = Globals.dbContext.Teachers.Where(t => t.Availability == true).Count();
            //checking for courses that are ongoing
            var countCourses = Globals.dbContext.Courses.Where(c => (c.StartDate <= DateTime.Today && c.EndDate >= DateTime.Today)).Count();
            // MessageBox.Show($"here {countStudents}, avail: {countTeachers} found courses: {countCourses}");
            LblStudentsEnrolled.Content = countStudents;
            LblTeachersAvailable.Content = countTeachers;
            LblCoursesAvailable.Content = countCourses;
        }
    }
}
