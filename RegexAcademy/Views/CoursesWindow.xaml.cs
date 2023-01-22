using RegexAcademy.Models;
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

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {

            CourseAddEdit inputDialog = new CourseAddEdit();
            if (inputDialog.ShowDialog() == true)
            {
                LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
            }
        }

        private void BtnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Course selectedCourse = LvCourses.SelectedItem as Course;

                if (selectedCourse == null) { return; }

                var result = MessageBox.Show("Are you sure you want to unregister \n" + selectedCourse.CourseId + "- " + selectedCourse.CourseName + "?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) { return; }
                Globals.dbContext.Courses.Remove(selectedCourse);

                Globals.dbContext.SaveChanges();

                LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error reading from database \n " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAssignStudents_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = LvCourses.SelectedItem as Course;
            AssignStudents assignDialog = new AssignStudents(selectedCourse);
            if (assignDialog.ShowDialog() == true)
            {
                Globals.dbContext.SaveChanges();
            }
        }

        private void BtnAssignTeacher_Click(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = LvCourses.SelectedItem as Course;
            AssignTeacher assignDialog = new AssignTeacher(selectedCourse);
            if (assignDialog.ShowDialog() == true)
            {
                Globals.dbContext.SaveChanges();
            }
        }

        private void BtnEditCourse_Click(object sender, RoutedEventArgs e)
        {

            Course selectedCourse = LvCourses.SelectedItem as Course;
            CourseAddEdit inputDialog = new CourseAddEdit(selectedCourse);

            if (inputDialog.ShowDialog() == true)
            {
                LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
            }
        }
    }
}
