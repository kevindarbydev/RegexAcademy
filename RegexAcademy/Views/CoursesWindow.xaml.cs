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
            Models.Course selectedCourse = LvCourses.SelectedItem as Models.Course;
            AssignStudents assignDialog = new AssignStudents(selectedCourse);
            if(assignDialog.ShowDialog() == true)
            {
                Globals.dbContext.SaveChanges();
            }
        }

        private void BtnAssignTeacher_Click(object sender, RoutedEventArgs e)
        {
            Models.Course selectedTeacher = LvCourses.SelectedItem as Models.Course;
            AssignTeacher assignDialog = new AssignTeacher(selectedTeacher);
            if(assignDialog.ShowDialog() == true)
            {
                Globals.dbContext.SaveChanges();
            }
        }
    }
}
