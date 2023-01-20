using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RegexAcademy.Views
{
    /// <summary>
    /// Interaction logic for CoursesWindow.xaml
    /// </summary>
    public partial class CoursesWindow : Page
    {
        public CoursesWindow()
        {
            InitializeComponent();
            LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
        }

        private void BtnAddCourse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CourseAdd inputDialog = new CourseAdd();
            if (inputDialog.ShowDialog() == true)
            {
                LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
            }
        }

        private void BtnAssignStudents_Click(object sender, RoutedEventArgs e)
        {
            AssignStudens assignDialog = new AssignStudens();
            if(assignDialog.ShowDialog() == true)
            {
                Globals.dbContext.SaveChanges();
            }
        }
    }
}
