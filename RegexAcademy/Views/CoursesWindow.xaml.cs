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
            try
            {
                Globals.dbContext = new RegexAcademyDbContext(); // Exceptions
                LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void BtnAddCourse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CourseAdd inputDialog = new CourseAdd();
            if (inputDialog.ShowDialog() == true)
            {
                LvCourses.ItemsSource = Globals.dbContext.Courses.ToList();
            }
        }
    }
}
